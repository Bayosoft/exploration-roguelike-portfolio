using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExplorationRoguelike.CombatStateComponent;

namespace ExplorationRoguelike
{
    public class AbilityComponent : MonoBehaviour
    {
        public KnownAbilitiesSO KnownAbilities;

        public bool TryActivateAbility(Ability ability, IEnumerable<Character> targets, ActivationType activationType)
        {
            /*if(!CanActivateAbility(ability, ))
              {
                return false;
             }*/

            ability.Activate(this, targets, activationType);

            return true;
        }

        public bool CanActivateAbility(Character target, Ability ability)
        {
            // Logic to see if the ability can be used

            return false;
        }
    }
}
