using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{

    [CreateAssetMenu(fileName = "Backstab", menuName = "Abilities/Combat/Backstab")]
    public class BackstabAbility : GameplayAbility
    {
        public int MinDamage, MaxDamage;
        // property indicating its a card/combat ability.
        // tags for determining status modifiers.

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            // TODO:  instigator.StatusComponent.ApplyModifiers(this); or does this happen sooner?

          //  AbilityExtensions.DamageMultipleTargets(instigator, targets, AbilityExtensions.GetRandomDamage(MinDamage, MaxDamage));
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
         //   AbilityExtensions.DamageSingleTarget(instigator, target, AbilityExtensions.GetRandomDamage(MinDamage, MaxDamage));
        }
    }
}
