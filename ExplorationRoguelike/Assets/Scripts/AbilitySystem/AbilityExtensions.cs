using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilityExtensions
    {
        public static float GetRandomDamage(float min, float max)
        {
           return Random.Range(min, max);
        }

        public static void DamageMultipleTargets(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets, int damage)
        {
            foreach (AbilitySystemComponent target in targets)
            {
                var healthComponent = target.GetComponent<HealthComponent>();
                if (healthComponent)
                {
                    healthComponent.ReduceHealthBy(damage);
                }
            }
        }

        public static void DamageSingleTarget(AbilitySystemComponent instigator, AbilitySystemComponent target, float damage)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if(healthComponent)
            {
                healthComponent.ReduceHealthBy(damage);
            }
        }
    }
}
