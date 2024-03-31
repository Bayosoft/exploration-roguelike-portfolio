using System.Collections.Generic;
using ExplorationRoguelike.GameplayTags;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities
{
    [CreateAssetMenu(fileName = "Instant Damage", menuName = "Abilities/Instant Damage")]
    public class InstantDamageAbility : GameplayAbility, IIntentfulAbility, IModifiable
    {
        [SerializeField]
        private GameplayTagContainer damageTags = new();
        
        [SerializeField]
        private float damage;

        [field: SerializeField]
        public GameplayTag IntentTag { get; private set; }

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            var damageMagnitude = CalculateModifiers(instigator);
            foreach (var target in targets)
            {
                AbilityExtensions.DamageSingleTarget(instigator, target, damageMagnitude);
            }
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            var damageMagnitude = CalculateModifiers(instigator);
            AbilityExtensions.DamageSingleTarget(instigator, target, damageMagnitude);
        }

        public event OnModifiersCalculated OnModifiersCalculated;
        public float CalculateModifiers(AbilitySystemComponent source)
        {
            var modifiedDamage = source.CalculateAggregatedModifiers(damage, damageTags);

            OnModifiersCalculated?.Invoke(modifiedDamage);

            return modifiedDamage;
        }
    }
}
