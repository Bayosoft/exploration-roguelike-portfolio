using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.GUI.Combat;
using Godot;

namespace ExplorationRoguelike
{
    public partial class GameState : Node
    {
        private CombatState _combatState;
        public Player player; 
        public CombatNpc enemy;

        public override void _Ready()
        {
            base._Ready();
            InitializeCombat();
        }
        private void InitializeCombat()
        {
            // Set up combat
            _combatState.Initialize(player, enemy);
        }

    }
}
