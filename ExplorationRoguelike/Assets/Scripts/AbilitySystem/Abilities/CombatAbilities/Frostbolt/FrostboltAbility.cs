using System.Collections.Generic;
using ExplorationRoguelike.GameplayEffects;
using ExplorationRoguelike.GameplayTags;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.Frostbolt
{

    [CreateAssetMenu(fileName = "Frostbolt", menuName = "Abilities/Combat/Frostbolt")]
    public class FrostboltAbility : CardAbility
    { 
        [SerializeField]
        private GameplayTagContainer damageTags = new();
        [SerializeField]
        private float damage = 0f;

        public GameplayEffect gameplayEffect;

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            GameplayEffectSpecification spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);

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
                float damageMagnitude = instigator.CalculateAggregatedModifiers(damage, damageTags);

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
