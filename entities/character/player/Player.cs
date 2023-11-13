using ExplorationRoguelike.AbilitySystem.CardSystem;
using ExplorationRoguelike.Combat;
using Godot;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    [GlobalClass]
    public partial class Player : Character, ICombatant
    {
        [Export]
        private HealthComponent healthComponent;
        public HealthComponent HealthComponent => healthComponent;

        [Export]
        private TurnComponent turnComponent;
        public TurnComponent TurnComponent => turnComponent;

        [Export]
        private CardDeckComponent cardDeckComponent;
        public CardDeckComponent CardDeckComponent => cardDeckComponent;
    }
}
