using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DistractAbility : GameplayAbility
    {
        // StatusEffect 
        [SerializeField]
        private int _durationInTurns;
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
