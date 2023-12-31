using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System;
using ExplorationRoguelike.GameplayEffects;

namespace ExplorationRoguelike.Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "Character/New Character")]
    public class CharacterData : ScriptableObject, IAbilityEntity
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

        public event EventHandler<AbilityGrantedEventArgs> AbilityGranted;

        public void GrantAbility(GameplayAbility ability)
        {
            if(ability.IsUnique && Abilities.Contains(ability))
            {
                return;
            }

            Abilities.Add(ability);
            AbilityGranted?.Invoke(this, new AbilityGrantedEventArgs(ability));
        }

        [SerializeField][CanBeNull]
        private CharacterHealth health;
        public virtual CharacterHealth Health => health;

        public ActiveGameplayEffectContainer ActiveGameplayEffects;
    }
}
