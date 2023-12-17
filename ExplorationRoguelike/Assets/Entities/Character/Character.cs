using ExplorationRoguelike.AbilitySystem;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] private CharacterData characterData;

        public CharacterData CharacterData
        {
            get => characterData;
            set => characterData = value;
        }

        [SerializeField] private AbilitySystemComponent abilitySystemComponent;
        public AbilitySystemComponent AbilitySystemComponent => abilitySystemComponent;
    }
}