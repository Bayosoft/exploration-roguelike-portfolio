using System.Collections.Generic;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.GameplayEffects;
using Godot;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities
{

    public partial class SelfStatusEffectAbility : GameplayAbility
    {
        [Export] private GameplayEffect gameplayEffect;

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<Character> targets)
        {
            var spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);

            if (spec != null)
            {
                instigator.ApplyGameplayEffectSpecToSelf(spec);
            }
            // Ability Fired event.
        }

        public override void Activate(AbilitySystemComponent instigator, Character target)
        {
            var spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);
            instigator.ApplyGameplayEffectSpecToSelf(spec);
        }
    }
}
