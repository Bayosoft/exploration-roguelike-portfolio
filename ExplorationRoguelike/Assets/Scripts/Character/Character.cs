using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterSO _characterData;
        public CharacterSO CharacterData { get => _characterData; }

        [SerializeField]
        private AbilityComponent _abilityComponent;
        public AbilityComponent AbilityComponent { get => _abilityComponent; }
    }
}
