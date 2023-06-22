using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        [SerializeField]
        private Character _owner;

        public Dictionary<GameplayTag, List<StatusEffectData>> ActiveStatusEffectsByTag;
        public List<AbilityData> GrantedAbilities { get => _owner.CharacterData.Abilities; }

        public bool TryActivateAbility(AbilityData ability, IEnumerable<Character> targets)
        {
            /*if(!CanActivateAbility(ability, ))
              {
                return false;
             }*/

            ability.Activate(_owner, targets);

            return true;
        }

        public bool CanActivateAbility(Character target, AbilityData ability)
        {
            // Logic to see if the ability can be used

            return false;
        }
    }
}
