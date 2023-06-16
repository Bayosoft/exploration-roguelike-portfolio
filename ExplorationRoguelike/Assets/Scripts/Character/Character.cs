using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterData _characterData;
        public CharacterData CharacterData { get => _characterData; }

        [SerializeField]
        private AbilityComponent _abilityComponent;
        public AbilityComponent AbilityComponent { get => _abilityComponent; }
    }
}
