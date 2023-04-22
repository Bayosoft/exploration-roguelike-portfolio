using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilityComponent : MonoBehaviour
    {

        public KnownAbilitiesData KnownAbilities;
        public bool CanActivateAbility(object context, Ability ability)
        {
            // Logic to see if the ability can be used

            // Example: A fireball should only be useable in combat or when a scenario specifically accepts fireball.

            return false;   
        }
        public bool TryActivateAbility(object context, Ability ability)
        {
            // Logic to try and use the ability

            // Example: A fireball needs a target to be used on, and that target has to be alive. 

            return false;
        }

        public void ActivateAbility(object context, Ability ability)
        {
            // Logic to use the ability
            // Example: Shoot the fireball at the enemy.
        }
    }
}
