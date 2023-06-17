using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{

    [CreateAssetMenu(fileName = "Fireball", menuName = "Abilities/Combat/Fireball")]
    public class FireballAbility : AbilityData
    {
        public int MinDamage, MaxDamage;
        // property indicating its a card/combat ability.
        // tags for determining status modifiers.

        public override void Activate(Character instigator, IEnumerable<Character> targets)
        {
            // TODO:  instigator.StatusComponent.ApplyModifiers(this); or does this happen sooner?

            AbilityExtensions.DamageMultipleTargets(instigator, targets, AbilityExtensions.GetRandomDamage(MinDamage, MaxDamage));
        }

        public override void Activate(Character instigator, Character target)
        {
            AbilityExtensions.DamageSingleTarget(instigator, target, AbilityExtensions.GetRandomDamage(MinDamage, MaxDamage));
        }
    }
}
