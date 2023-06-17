using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        [SerializeField]
        private Character _owner;

        public List<AbilityData> GrantedAbilities { get => _owner.CharacterData.GrantedAbilities; }

        public bool TryActivateAbility(AbilityData ability, IEnumerable<Character> targets)
        {
            /*if(!CanActivateAbility(ability, ))
              {
                return false;
             }*/

            ability.Activate(_owner, targets);

            return true;
        }

        public bool CanActivateAbility(Character target, Ability ability)
        {
            // Logic to see if the ability can be used

            return false;
        }
    }
}
