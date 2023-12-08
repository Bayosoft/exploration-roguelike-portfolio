using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using Godot;
using System.Collections.Generic;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.Distract
{
    public partial class DistractAbility : GameplayAbility
    {
        // StatusEffect 
        [Export]
        private int durationInTurns;
        //
        // property indicating its a card/combat ability.
        // tags for determining status modifiers.

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<Character> targets)
        {
            // TODO:  instigator.StatusComponent.ApplyModifiers(this); or does this happen sooner?

          //  AbilityExtensions.ApplyStatusEffect(instigator, targets, effect);
        }

        public override void Activate(AbilitySystemComponent instigator, Character target)
        {
            //  AbilityExtensions.ApplyStatusEffect(instigator, target, effect);
        }
    }
}
