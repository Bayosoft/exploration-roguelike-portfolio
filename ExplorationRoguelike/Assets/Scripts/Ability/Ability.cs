using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class Ability
    {
        public AbilitySO AbilityData;
        public AbilityComponent Instigator;
        public List<Character> Targets;

        public Ability(AbilitySO abilityData)
        {
            AbilityData = abilityData;
        }

        /// <summary>
        /// Overridden by specific abilities
        /// </summary>
        /// <param name="instigator"></param>
        /// <param name="targets"></param>
        /// <param name="activationType"></param>
        public void Activate(AbilityComponent instigator, IEnumerable<Character> targets, ActivationType activationType)
        {
            // Make generic and take away activation type param
            if(activationType == ActivationType.COMBAT)
            {
                int damage = AbilityData.DealDamage();
                foreach (Character c in targets)
                {
                    var hc = c.GetComponent<HealthComponent>();
                    hc.ReduceHealthBy(damage);
                }
            }
        }
    }
}
