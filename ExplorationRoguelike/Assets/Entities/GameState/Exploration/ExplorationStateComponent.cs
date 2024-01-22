using System.Collections.Generic;
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

        public Dictionary<(int, int), ExplorationTile> MapGrid;

        private ExplorationTile selectedTile;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            gameState = FindFirstObjectByType<GameState>();
            MapGrid = tileSpawner.GenerateMap(/* map data object (scriptable object containing enemies, sprites, and events for the map */);         
        }

        public void OnExploreTile(ConcreteEventArgs eventArgs)
        {
            var exploreTileEventArgs = eventArgs.ValidateEventArgs<ExploreTileEventArgs>(eventArgs);

            ExplorationTile tile = exploreTileEventArgs.Tile;

            SelectTile(tile);
        }

        private void SelectTile(ExplorationTile tile)
        {
            selectedTile = tile;

            Camera.main.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, -10);

            if (tile is CombatExplorationTile combatTile)
            {
                gameState.SetCombat(combatTile.EnemyData.First());
                SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
                this.enabled = false;
                // When combat ends, re-enable..
            }
        }
    }
}
