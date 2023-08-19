using ExplorationRoguelike.AbilitySystem.CardSystem;
using ExplorationRoguelike.Combat;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    public class Player : Character, ICombatant
    {
        [SerializeField]
        private HealthComponent _healthComponent;
        public HealthComponent HealthComponent => _healthComponent;

        [SerializeField]
        private TurnComponent _turnComponent;
        public TurnComponent TurnComponent => _turnComponent;

        [SerializeField]
        private CardDeckComponent _cardDeckComponent;
        public CardDeckComponent CardDeckComponent => _cardDeckComponent;
    }
}
