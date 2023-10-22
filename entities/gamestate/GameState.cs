using System.Collections.Generic;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using Godot;

namespace ExplorationRoguelike
{
    public partial class GameState : Node
    {
        private CombatStateComponent _combatStateComponent;
        public Node player;
        public Node enemy;

        // TODO: This might not be needed at all anymore.
/*        void Awake()
        {
            // UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        public void SetCombatants(GameObject playerPrefab, GameObject enemyPrefab)
        {
            player = playerPrefab;
            enemy = enemyPrefab;
        }
        
        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "CombatScene")
            {
                _combatStateComponent = FindObjectOfType<CombatStateComponent>();
                InitializeCombat();
            }
        }
        private void InitializeCombat()
        {
            _combatStateComponent.Initialize(player, enemy);
        }
*/

    }
}
