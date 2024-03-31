using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.GameplayTags;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Instant Healing", menuName = "Abilities/Instant Healing")]
    public class InstantHealingAbility : GameplayAbility, IIntentfulAbility, IModifiable
    {
        [SerializeField]
        private GameplayTagContainer healingTags = new();

        [SerializeField]
        private float healing;

        [field: SerializeField]
        public GameplayTag IntentTag { get; private set; }

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            var healingMagnitude = CalculateModifiers(instigator);
            foreach(var target in targets)
            {
                AbilityExtensions.HealOther(instigator, target, healingMagnitude);
            }
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            var healingMagnitude = CalculateModifiers(instigator);
            AbilityExtensions.HealOther(instigator, target, healingMagnitude);
        }

        public event OnModifiersCalculated OnModifiersCalculated;
        public float CalculateModifiers(AbilitySystemComponent source)
        {
            var modifiedHealing = source.CalculateAggregatedModifiers(healing, healingTags);

            OnModifiersCalculated?.Invoke(modifiedHealing);

            return modifiedHealing;
        }
    }
}