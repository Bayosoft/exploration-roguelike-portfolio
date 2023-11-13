using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.GUI.Combat;
using Godot;

namespace ExplorationRoguelike
{
    public partial class GameState : Node
    {
        private CombatState _combatState;
        private Player player;  
        private CombatNpc enemy;

        public override void _Ready()
        {
            base._Ready();

            var combatScene = ResourceLoader.Load<PackedScene>("res://entities/gamestate/combat/Combat.tscn");
            _combatState = combatScene.Instantiate<CombatState>();

            AddChild(_combatState);

            var playerScene = ResourceLoader.Load<PackedScene>("res://entities/character/player/Player.tscn");
            player = playerScene.Instantiate<Player>();


            var enemyScene = ResourceLoader.Load<PackedScene>("res://entities/character/enemy/Enemy.tscn");
            enemy = enemyScene.Instantiate<CombatNpc>();

            InitializeCombat();
        }
        private void InitializeCombat()
        {
            // Set up combat
            _combatState.Initialize(player, enemy);
        }

    }
}
