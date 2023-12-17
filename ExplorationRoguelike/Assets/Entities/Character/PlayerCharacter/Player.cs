using ExplorationRoguelike.AbilitySystem.CardSystem;
using ExplorationRoguelike.Combat;
using UnityEngine;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class Player : Character, ICombatant
    {
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
