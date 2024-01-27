using Cinemachine;
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
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            
            gameState = FindFirstObjectByType<GameState>();
            MapGrid = tileSpawner.GenerateMap(/* map data object (scriptable object containing enemies, sprites, and events for the map */);         
        }

        public void OnExploreTile(ConcreteEventArgs eventArgs)
        {
            var exploreTileEventArgs = eventArgs.ValidateEventArgs<ExploreTileEventArgs>(eventArgs);

            ExplorationTile tile = exploreTileEventArgs.Tile;

            SelectTile(tile);
        }

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        private void SelectTile(ExplorationTile tile)
        {
            selectedTile = tile;

            virtualCamera.Follow = tile.transform;

            if (tile is CombatExplorationTile combatTile)
            {
                gameState.SetCombat(combatTile.EnemyData.First());
                gameObject.SetActive(false);
                this.enabled = false;
            }
        }
        private void OnSceneUnloaded(Scene current)
        {
            gameObject.SetActive(true);
            this.enabled = true;
        }

        private void OnDestroy()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}
