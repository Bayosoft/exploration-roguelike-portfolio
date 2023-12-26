using ExplorationRoguelike.GameplayTags;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.Distract
{
    public class DistractAbility : GameplayAbility, IIntentfulAbility
    {
        // StatusEffect 
        [SerializeField]
        private int durationInTurns;

        [field: SerializeField]
        public GameplayTag IntentTag { get; private set; }

        //
        // property indicating its a card/combat ability.
        // tags for determining status modifiers.

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            // TODO:  instigator.StatusComponent.ApplyModifiers(this); or does this happen sooner?

          //  AbilityExtensions.ApplyStatusEffect(instigator, targets, effect);
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            //  AbilityExtensions.ApplyStatusEffect(instigator, target, effect);
        }
    }
}
