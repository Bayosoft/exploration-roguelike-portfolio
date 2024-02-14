using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ExplorationStateComponent : MonoBehaviour
    {
        [SerializeField]
        private ExplorationTileSpawner tileSpawner;

        [SerializeField]
        private MapContents mapContents;

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        private GameState gameState;

        public Dictionary<(int x, int y), ExplorationTile> MapGrid;

        private ExplorationTile selectedTile;

        private List<ExplorationTile> selectableTiles;

        // Start is called before the first frame update
        void Start()
        {

            gameState = FindFirstObjectByType<GameState>();
            MapGrid = tileSpawner.GenerateMap(mapContents);

            Destroy(tileSpawner);

            virtualCamera.transform.position = new Vector3(MapGrid.First().Value.transform.position.x, virtualCamera.transform.position.y, -10);
            virtualCamera.m_Lens.OrthographicSize = 600;

            selectableTiles = new List<ExplorationTile>();

            for (int column = 0; column < 7; column++)
            {
                MapGrid.TryGetValue((0, column), out var tile);

                if (tile != null)
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

            StartCoroutine(SelectTile(tile));
        }

        private IEnumerator SelectTile(ExplorationTile tile)
        {
            virtualCamera.Follow = tile.transform;

            if (selectedTile == null)
            {
                int endSize = 240;
                float moveTime = 50f;
                float elapsedTime = 0f;

                while (elapsedTime < moveTime)
                {
                    elapsedTime += Time.deltaTime;

                    int lerpedSize = (int)Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, endSize, (elapsedTime / moveTime));

                    virtualCamera.m_Lens.OrthographicSize = lerpedSize;

                }
            }
            // TODO: Fix yield return null not letting me get here..
            yield return new WaitForSecondsRealtime(1);

            selectedTile = tile;
            tile.Selected();
        }

/*        private IEnumerator ActivateTile(ExplorationTile tile)
        {

        }*/
    }
}
