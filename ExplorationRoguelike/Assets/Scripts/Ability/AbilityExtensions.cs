using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilityExtensions
    {
        public static int GetRandomDamage(int min, int max)
        {
           return Random.Range(min, max);
        }

        public static void DamageMultipleTargets(Character instigator, IEnumerable<Character> targets, int damage)
        {
            foreach (Character target in targets)
            {
                var hc = (target as ICombatant).HealthComponent;
                hc.ReduceHealthBy(damage);
            }
        }

        public static void DamageSingleTarget(Character instigator, Character target, int damage)
        {
            var hc = (target as ICombatant).HealthComponent;
            hc.ReduceHealthBy(damage);
        }


    }
}
