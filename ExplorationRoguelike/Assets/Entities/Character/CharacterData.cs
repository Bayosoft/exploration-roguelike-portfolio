using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "Characters/New Character")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        private new string name;
        public string Name => name;

        [SerializeField]
        private Sprite gameImage;
        public Sprite GameImage => gameImage;

        [SerializeField]
        private Sprite uiImage;
        public Sprite UiImage => uiImage;

        [SerializeField]
        private List<GameplayAbility> abilities;
        public List<GameplayAbility> Abilities => abilities;

        [SerializeField][CanBeNull]
        private CharacterHealth health;
        public virtual CharacterHealth Health => health;
    }
}
