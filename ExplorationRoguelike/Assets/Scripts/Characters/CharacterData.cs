using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    public abstract class CharacterData : ScriptableObject
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
        private HealthData health;
        public virtual HealthData Health => health;
    }
}
