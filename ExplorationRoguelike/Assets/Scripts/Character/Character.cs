using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterData _characterData;
        public CharacterData CharacterData { get => _characterData; }

        [SerializeField]
        private AbilitySystemComponent _abilitySystemComponent;
        public AbilitySystemComponent AbilitySystemComponent { get => _abilitySystemComponent; }
    }
}
