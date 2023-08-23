using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    public abstract class CharacterData : ScriptableObject
    {
        [SerializeField]
        private new string name;
        public string Name { get => name; }

        [SerializeField]
        private Sprite gameImage;
        public Sprite GameImage { get => gameImage; }

        [SerializeField]
        private Sprite uiImage;
        public Sprite UiImage { get => uiImage; }

        [SerializeField]
        private List<GameplayAbility> abilities;
        public List<GameplayAbility> Abilities { get => abilities; }
    }
}
