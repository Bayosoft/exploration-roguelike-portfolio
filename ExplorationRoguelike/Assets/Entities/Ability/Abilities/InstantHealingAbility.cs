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
            foreach(var target in targets)
            {
                var healingMagnitude = CalculateModifiers(instigator, target);

                AbilityExtensions.HealOther(instigator, target, healingMagnitude);
            }
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            var healingMagnitude = CalculateModifiers(instigator, target);
            AbilityExtensions.HealOther(instigator, target, healingMagnitude);
        }

        public event OnModifiersCalculated OnModifiersCalculated;
        public float CalculateModifiers(AbilitySystemComponent source, AbilitySystemComponent target)
        {
            var modifiedHealing = source.CalculateAggregatedModifiers(healing, healingTags, GameplayEffects.GameplayModifierDirection.Outgoing);
            modifiedHealing = source.CalculateAggregatedModifiers(modifiedHealing, healingTags, GameplayEffects.GameplayModifierDirection.Incoming);

            OnModifiersCalculated?.Invoke(modifiedHealing);

            return modifiedHealing;
        }
    }
}