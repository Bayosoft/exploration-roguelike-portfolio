using ExplorationRoguelike.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem
{
    public class AbilityExtensions
    {
        public static int GetRandomDamage(int min, int max)
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
            if (healthComponent)
            {
                healthComponent.ReduceHealthBy(damage);
            }
        }

        public static void HealSelf(AbilitySystemComponent instigator, float healing)
        {
            var healthComponent = instigator.GetComponent<HealthComponent>();
            if (healthComponent)
            {
                healthComponent.IncreaseHealthBy(healing);
            }
        }
        public static void HealOther(AbilitySystemComponent instigator, AbilitySystemComponent target, float healing)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent)
            {
                healthComponent.IncreaseHealthBy(healing);
            }
        }
    }
}
