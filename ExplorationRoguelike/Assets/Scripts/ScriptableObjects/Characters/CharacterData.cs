using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class CharacterData : ScriptableObject
    {
        [SerializeField]
        private string _name;
        public string Name { get => _name; }

        [SerializeField]
        private Sprite _gameImage;
        public Sprite GameImage { get => _gameImage; }

        [SerializeField]
        private Sprite _uiImage;
        public Sprite UiImage { get => _uiImage; }

        [SerializeField]
        private List<GameplayAbility> _abilities;
        public List<GameplayAbility> Abilities { get => _abilities; }
    }
}
