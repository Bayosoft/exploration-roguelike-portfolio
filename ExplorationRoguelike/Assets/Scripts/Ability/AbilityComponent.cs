using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExplorationRoguelike.CombatStateComponent;

namespace ExplorationRoguelike
{
    public class AbilityComponent : MonoBehaviour
    {
        // public List<AbilitySO> KnownAbilities { get => GetComponent<Character>().CharacterData.Abilities; }
        public KnownAbilitiesData KnownAbilities;

        public bool TryActivateAbility(AbilityData ability, IEnumerable<Character> targets)
        {
            /*if(!CanActivateAbility(ability, ))
              {
                return false;
             }*/

            ability.Activate(this, targets);

            return true;
        }

        public bool CanActivateAbility(Character target, Ability ability)
        {
            // Logic to see if the ability can be used

            return false;
        }
    }
}
