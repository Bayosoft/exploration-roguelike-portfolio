using ExplorationRoguelike.AbilitySystem.CardSystem;
using ExplorationRoguelike.Combat;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class Player : Character, ICombatant
    {
        public PlayerCharacterData PlayerCharacterData
        {
            get => base.characterData as PlayerCharacterData;
            set => base.characterData = value;
        }

        [SerializeField]
        private HealthComponent healthComponent;
        public HealthComponent HealthComponent => healthComponent;

        [SerializeField]
        private TurnComponent turnComponent;
        public TurnComponent TurnComponent => turnComponent;

        [SerializeField]
        private CardDeckComponent cardDeckComponent;
        public CardDeckComponent CardDeckComponent => cardDeckComponent;
    }
}
