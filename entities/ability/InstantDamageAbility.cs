using System.Collections.Generic;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.GameplayTags;
using Godot;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities
{
    [GlobalClass]
    public partial class InstantDamageAbility : GameplayAbility, IModifiable
    {
        [Export]
        private GameplayTagContainer damageTags = new();

        [Export]
        private float damage;

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<Character> targets)
        {
            var damageMagnitude = CalculateModifiers(instigator);
            foreach (var target in targets)
            {
                AbilityExtensions.DamageSingleTarget(instigator, target, damageMagnitude);
            }
        }

        public override void Activate(AbilitySystemComponent instigator, Character target)
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
