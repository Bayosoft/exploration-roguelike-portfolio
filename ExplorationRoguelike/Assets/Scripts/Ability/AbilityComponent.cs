using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExplorationRoguelike.CombatStateComponent;

namespace ExplorationRoguelike
{
    public class AbilityComponent : MonoBehaviour
    {

        public KnownAbilitiesSO KnownAbilities;
        public bool TryActivateAbility( context, Ability ability)
        {
            if(!CanActivateAbility(ability, ))
            {
                ActivateCombatAbility(ability);
            }
            else
            {
                ActivateAbility(ability);
            }
            // Logic to try and use the ability

            // Example: A fireball needs a target to be used on, and that target has to be alive. 

            return false;
        }

        public bool CanActivateAbility(Character target, Ability ability)
        {
            // Logic to see if the ability can be used
            // Example: A fireball should only be useable in combat or when a scenario specifically accepts fireball.

            ability.
            return false;
        }

        public void ActivateAbility(object context, Ability ability)
        {
            // Logic to use the ability
            // Example: Shoot the fireball at the enemy.
        }
    }
}
