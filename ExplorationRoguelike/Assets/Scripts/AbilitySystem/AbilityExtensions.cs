using ExplorationRoguelike.Characters;
using System;
using System.Collections.Generic;

namespace ExplorationRoguelike.AbilitySystem
{
    public class AbilityExtensions
    {
        public static int GetRandomDamage(int min, int max)
        {
           return new Random().Next(min, max);
        }

        // TODO: Affect target's health
        public static void DamageMultipleTargets(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets, int damage)
        {
            foreach (AbilitySystemComponent target in targets)
            {
               /* var healthComponent = target.GetComponent<HealthComponent>();
                if (healthComponent)
                {
                    healthComponent.ReduceHealthBy(damage);
                }*/
            }
        }

        public static void DamageSingleTarget(AbilitySystemComponent instigator, AbilitySystemComponent target, float damage)
        {
            /*var healthComponent = target.GetComponent<HealthComponent>();
            if(healthComponent)
            {
                healthComponent.ReduceHealthBy(damage);
            }*/
        }
    }
}
