using ExplorationRoguelike.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ExplorationTileSpawner : MonoBehaviour
    {
        public GameObject WeakEnemyTilePrefab, AverageEnemyTilePrefab, BossEnemyTilePrefab,
            EventTilePrefab, TreasureTilePrefab;

        public List<CharacterData> WeakEnemies; // TODO: this kind of list should be passed into GenerateMap as part of a scriptable object containing the map's data.

        [SerializeField]
        private int maxStartingTiles;

        [SerializeField]
        private int maxPaths;

        [SerializeField]
        private Vector2Int mapSize;

        //private Dictionary<int[,], ExplorationTile> ExplorationGrid;

        private ExplorationTile[,] mapGrid; // row = row, column = column
        public void GenerateMap()
        {
            mapGrid = new ExplorationTile[mapSize.x, mapSize.y];

            int startingTiles = Random.Range(1, maxStartingTiles);

            // TODO: Refactor to create full paths based on the starting paths (generate a full path in one go, then next path)
            for (int row = 0; row < mapSize.x; row++)
            {
                if (row == 0)
                {
                    SpawnStartingTile();
                    continue;
                }

                int tileAmount = Random.Range(1, mapSize.y);
                int spawnedTiles = 0;

                for (int column = 0; column < mapSize.y; column++)
                {
                    if(!CanCreatePath(row, column))
                    {
                        continue;
                    }

                    // TODO: Determine if we create a new tile. (if we always do it as soon as it is possible, the paths will hug the left)
                    if (spawnedTiles == tileAmount)
                    {
                        break;
                    }
                    try
                    {
                        if (mapGrid[row - 1, column] == null && mapGrid[row - 1, column - 1] == null && mapGrid[row - 1, column + 1] == null)
                        {
                            continue;
                        }
                    }
                    catch
                    {
                        continue;
                    }

                    SpawnCombatTile(WeakEnemyTilePrefab, new Vector2Int(row, column));
                    spawnedTiles++;

                }
            }
        }

        private bool CanCreatePath(int row, int column)
        {
            if (row == 0)
            {
                return false;
            }

            int prevRow = row - 1;

            if (mapGrid[prevRow, column] != null)
            {
                return true;
            }

            if (column == 0) // Far left
            {
                return mapGrid[prevRow, column + 1] != null;
            }

            if (column == mapSize.y - 1) // Far right
            {
                return mapGrid[prevRow, column - 1] != null;
            }

            return false;
        }

        private void SpawnStartingTile()
        {
            int y = Random.Range(0, mapSize.y);

            SpawnCombatTile(WeakEnemyTilePrefab, new Vector2Int(0, y));
        }

        private void SpawnCombatTile(GameObject tile, Vector2Int position)
        {
            var tileObject = Instantiate(tile);
            tileObject.transform.parent = this.transform;

            tileObject.transform.position = new Vector2(position.y * 100, position.x * 100);

            // TODO: Randomly select from list of enemies
            var combatTile = tileObject.GetComponent<CombatExplorationTile>();

            combatTile.EnemyData = WeakEnemies;

            mapGrid[position.x, position.y] = combatTile;
        }
    }
}