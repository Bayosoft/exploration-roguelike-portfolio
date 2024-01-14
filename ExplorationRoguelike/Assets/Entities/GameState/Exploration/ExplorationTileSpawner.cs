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

        private ExplorationTile[,] mapGrid; // x = row, y = column
        public void GenerateMap()
        {
            mapGrid = new ExplorationTile[mapSize.x, mapSize.y];


            // TODO: Refactor to create full paths based on the starting paths (generate a full path in one go, then next path)
            for (int x = 0; x < mapSize.x; x++)
            {
                if (x == 0)
                {
                    SpawnStartingTiles();
                    continue;
                }

                int tileAmount = Random.Range(1, mapSize.y);
                int spawnedTiles = 0;

                for (int y = 0; y < mapSize.y; y++)
                {
                    if (spawnedTiles == tileAmount)
                    {
                        break;
                    }
                    try
                    {
                        if (mapGrid[x - 1, y] == null && mapGrid[x - 1, y - 1] == null && mapGrid[x - 1, y + 1] == null)
                        {
                            continue;
                        }
                    }
                    catch
                    {
                        continue;
                    }

                    SpawnCombatTile(WeakEnemyTilePrefab, new Vector2Int(x, y));
                    spawnedTiles++;

                }
            }
        }

        private void SpawnStartingTiles()
        {
            int startingTiles = Random.Range(1, maxStartingTiles);

            for (int i = 0; i < startingTiles; i++)
            {
                int y = Random.Range(0, mapSize.y);

                SpawnCombatTile(WeakEnemyTilePrefab, new Vector2Int(0, y));
            }
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