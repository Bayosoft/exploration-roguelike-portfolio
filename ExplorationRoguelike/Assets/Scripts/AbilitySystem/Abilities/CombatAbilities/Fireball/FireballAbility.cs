using ExplorationRoguelike.GameplayEffects;
using ExplorationRoguelike.Scripts.GameplayTags;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities
{

    [CreateAssetMenu(fileName = "Fireball", menuName = "Abilities/Combat/Fireball")]
    public class FireballAbility : CardAbility, IModifiable
    {
        // Added in designer
        [SerializeField]
        private GameplayTagContainer _ignitedTagContainer = new();

        [SerializeField]
        private GameplayTagContainer damageTags = new();
        [SerializeField]
        private float damage = 0f;

        public GameplayEffect GameplayEffect;

        public event OnModifiersCalculated OnModifiersCalculated;

        public float CalculateModifiers(AbilitySystemComponent source)
        {
            float modifiedDamage = source.CalculateAggregatedModifiers(damage, damageTags);

            OnModifiersCalculated?.Invoke(modifiedDamage);

            return modifiedDamage;
        }
        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            GameplayEffectSpecification spec = instigator.MakeOutgoingEffectSpec(GameplayEffect);

//             if(spec != null)
//             {
//                 foreach (AbilitySystemComponent target in targets)
//                 {
//                     if (target.HasAll(_ignitedTagContainer))
//                     {
//                         spec.Duration += 5;
//                     }
//                     instigator.ApplyGameplayEffectSpecToSelf(spec);
//                 }
//             }

            // Temp damage testing
            foreach (AbilitySystemComponent target in targets)
            {
                float damageMagnitude = CalculateModifiers(instigator);
                AbilityExtensions.DamageSingleTarget(instigator, target, damageMagnitude);
            }
            // Ability Fired event.
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            // Temp damage testing
            float damageMagnitude = instigator.CalculateAggregatedModifiers(damage, damageTags);

            AbilityExtensions.DamageSingleTarget(instigator, target, damageMagnitude);
        }
    }
}
