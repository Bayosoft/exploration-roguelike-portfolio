using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExplorationRoguelike
{
    public class ExplorationStateComponent : MonoBehaviour
    {
        [SerializeField]
        private ExplorationTileSpawner tileSpawner;
        
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        private GameState gameState;

        public Dictionary<(int x, int y), ExplorationTile> MapGrid;

        private ExplorationTile selectedTile;

        private List<ExplorationTile> selectableTiles;

        // Start is called before the first frame update
        void Start()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            
            gameState = FindFirstObjectByType<GameState>();
            MapGrid = tileSpawner.GenerateMap(/* map data object (scriptable object containing enemies, sprites, and events for the map */);
            virtualCamera.transform.position = new Vector3(MapGrid.First().Value.transform.position.x, virtualCamera.transform.position.y, -10);
            virtualCamera.m_Lens.OrthographicSize = 600;

            selectableTiles = new List<ExplorationTile>();

            for(int column = 0; column < 7; column++)
            {
                MapGrid.TryGetValue((0, column), out var tile);

                if(tile != null)
                {
                    selectableTiles.Add(tile);
                    tile.InReach();
                }
            }
        }

        public void OnExploreTile(ConcreteEventArgs eventArgs)
        {
            var exploreTileEventArgs = eventArgs.ValidateEventArgs<ExploreTileEventArgs>(eventArgs);

            ExplorationTile tile = exploreTileEventArgs.Tile;

            SelectTile(tile);
        }

        private void SelectTile(ExplorationTile tile)
        {
            if(selectedTile == null)
            {
                virtualCamera.m_Lens.OrthographicSize = 240; //TODO: Lerp to 200 so that its smooth.
            }

            selectedTile = tile;

            virtualCamera.Follow = tile.transform;

            tile.Selected();

            
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
