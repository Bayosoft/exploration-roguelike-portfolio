using ExplorationRoguelike.Characters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ExplorationTileSpawner : MonoBehaviour
    {
        public GameObject basicEnemyTilePrefab, eliteEnemyTilePrefab, BossEnemyTilePrefab,
            EventTilePrefab, TreasureTilePrefab;

        private MapContents mapContents;

        [SerializeField]
        private int maxStartingTiles;

        [SerializeField]
        private int maxPaths;

        [SerializeField]
        private Vector2Int mapSize;

        public Dictionary<(int, int), ExplorationTile> GenerateMap(MapContents contents)
        {
            var mapGrid = new Dictionary<(int, int), ExplorationTile>();
            mapContents = contents;

            int startingTiles = UnityEngine.Random.Range(1, maxStartingTiles);
            int startingTileCount = 0;

            (int, int) sourcePosition = default;

            // TODO: Refactor to create full paths based on the starting paths (generate a full path in one go, then next path)
            for (int row = 0; row <= mapSize.x; row++)
            {
                if (row == 0 && startingTileCount < startingTiles)
                {
                    sourcePosition = SpawnStartingTile(mapGrid);
                    startingTileCount++;
                    continue;
                }

                List<(int, int)> viablePositions = new();

                for (int column = 0; column <= mapSize.y; column++)
                {
                    if (HasPathToSource(row, column, sourcePosition))
                    {
                        viablePositions.Add((row, column));
                    }
                }

                (int, int) tilePosition = viablePositions[UnityEngine.Random.Range(0, viablePositions.Count)];

                var tilePrefab = DetermineTileType(tilePosition);

                sourcePosition = SpawnTile(mapGrid, tilePrefab, tilePosition);

                DrawPath(mapGrid, sourcePosition);
            }

            return mapGrid;
        }
        private bool HasPathToSource(int row, int column, (int x, int y) sourcePosition)
        {
            if (row == 0)
            {
                return false;
            }

            int prevRow = row - 1;

            if (sourcePosition == (prevRow, column))
            {
                return true;
            }

            if (column == 0) // Far left
            {
                return sourcePosition == (prevRow, column + 1);
            }

            if (column == mapSize.y - 1) // Far right
            {
                return sourcePosition == (prevRow, column - 1);
            }

            if (sourcePosition == (prevRow, column - 1) || sourcePosition == (prevRow, column + 1))
            {
                return true;
            }
            return false;
        }

        private (int, int) SpawnStartingTile(Dictionary<(int, int), ExplorationTile> mapGrid)
        {
            int y = UnityEngine.Random.Range(0, mapSize.y);

            return SpawnTile(mapGrid, basicEnemyTilePrefab, (0, y));
        }

        private GameObject DetermineTileType((int row, int column) position)
        {
            int basicEnemyWeight = 5;
            int eliteEnemyWeight = 1;
            int randomEventWeight = 2;

            // if previous row has boss, set boss weight to 0..

            WeightedList<GameObject> weightedTiles = new()
            {
                { basicEnemyTilePrefab, basicEnemyWeight },
                { eliteEnemyTilePrefab, eliteEnemyWeight },
                { EventTilePrefab, randomEventWeight },
            };

            return weightedTiles.Next(); // Draw a random item from the list.
        }

        private (int, int) SpawnTile(Dictionary<(int, int), ExplorationTile> mapGrid, GameObject tile, (int x, int y) position)
        {
            var tileObject = Instantiate(tile);
            tileObject.transform.SetParent(this.transform, false);

            tileObject.transform.position = new Vector2(position.y * 100, position.x * 100);

            var tileType = tileObject.GetComponent<ExplorationTile>();

            if (tileType is CombatExplorationTile combatTile)
            {
                var enemyPool = mapContents.GetEnemyPool(combatTile.EnemyTier);   
                combatTile.SetRandomEnemy(enemyPool);

                tileType = combatTile;
            }
            if(tileType is RandomEventExplorationTile eventTile)
            {
                eventTile.SetRandomEvent(mapContents);

                tileType = eventTile;
            }

            mapGrid.TryAdd((position.x, position.y), tileType);

            return (position.x, position.y);
        }

        private void DrawPath(Dictionary<(int, int), ExplorationTile> mapGrid, (int x, int y) sourcePosition)
        {
            int prevRow = sourcePosition.x - 1;

            if (mapGrid.TryGetValue((prevRow, sourcePosition.y), out ExplorationTile pathableTile))
            {
                pathableTile.AddConnectedTile(mapGrid[sourcePosition]);
                // Draw path.
            }

            if (mapGrid.TryGetValue((prevRow, sourcePosition.y + 1), out pathableTile)) // Far left
            {
                if (pathableTile != null)
                {
                    pathableTile.AddConnectedTile(mapGrid[sourcePosition]);
                    // Draw path.
                }
                if (sourcePosition.y == 0)
                {
                    return;
                }
            }

            if (mapGrid.TryGetValue((prevRow, sourcePosition.y - 1), out pathableTile)) // Far right
            {
                if (pathableTile != null)
                {
                    pathableTile.AddConnectedTile(mapGrid[sourcePosition]);
                }
                // Draw path.

                if (sourcePosition.y == mapSize.y - 1)
                {
                    return;
                }
            }
        }
    }
}