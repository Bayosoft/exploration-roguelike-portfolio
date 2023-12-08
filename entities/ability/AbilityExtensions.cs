using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExplorationRoguelike.AbilitySystem
{
    public class AbilityExtensions
    {
        public static int GetRandomDamage(int min, int max)
        {
            return new Random().Next(min, max);
        }

        // TODO: Affect target's health
        public static void DamageMultipleTargets(AbilitySystemComponent instigator, IEnumerable<Character> targets, int damage)
        {
            foreach (ICombatant combatant in targets.Where(t => t is ICombatant))
            {
                combatant.HealthComponent.ReduceHealthBy(damage);
            }
        }

        public static void DamageSingleTarget(AbilitySystemComponent instigator, Character target, float damage)
        {
            if(target is ICombatant combatant)
            {
                combatant.HealthComponent.ReduceHealthBy(damage);
            }

        }
    }
}
