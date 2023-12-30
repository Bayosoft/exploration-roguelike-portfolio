using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class ExplorationStateComponent : MonoBehaviour
    {
        [SerializeField]
        private ExplorationTileSpawner tileSpawner;

        private GameState gameState;
        // Start is called before the first frame update
        void Start()
        {
            gameState = FindFirstObjectByType<GameState>();
            tileSpawner.GenerateMap(/* map data object (scriptable object containing enemies, sprites, and events for the map */);
        }

        public void OnExploreTile(ConcreteEventArgs eventArgs)
        {
            var exploreTileEventArgs = eventArgs.ValidateEventArgs<ExploreTileEventArgs>(eventArgs);

            ExplorationTile tile = exploreTileEventArgs.Tile;
            
            if (tile is CombatExplorationTile combatTile)
            {
                gameState.SetCombat(combatTile.EnemyData.First());
                SceneManager.LoadSceneAsync("CombatScene");
            }

        }
    }
}
