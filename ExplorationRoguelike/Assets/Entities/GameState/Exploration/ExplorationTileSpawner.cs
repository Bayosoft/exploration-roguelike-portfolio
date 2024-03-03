using ExplorationRoguelike.Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

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

            (int, int) sourcePosition = default;

            for (int paths = 0; paths <= startingTiles; paths++)
            {
                for (int row = 0; row <= mapSize.x; row++)
                {
                    if (row == 0)
                    {
                        int column = UnityEngine.Random.Range(0, mapSize.y);

                        if (!mapGrid.ContainsKey((0, column)))
                        {
                            SpawnTile(mapGrid, basicEnemyTilePrefab, (0, column));
                        }
                        
                        sourcePosition = (row, column);

                        continue;
                    }

                    List<(int, int)> viablePositions = new();

                    for (int column = 0; column <= mapSize.y; column++)
                    {
                        if (HasPathToSource(row, column, sourcePosition) && !mapGrid.ContainsKey((row, column)))
                        {
                            viablePositions.Add((row, column));
                        }
                    }

                    (int, int) tilePosition = viablePositions[UnityEngine.Random.Range(0, viablePositions.Count)];

                    var tilePrefab = DetermineTileType(tilePosition);

                    SpawnTile(mapGrid, tilePrefab, tilePosition);

                    sourcePosition = tilePosition;

                    DrawTilePaths(mapGrid, sourcePosition);
                }
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

        private bool SpawnTile(Dictionary<(int, int), ExplorationTile> mapGrid, GameObject tile, (int x, int y) position)
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
            if (tileType is RandomEventExplorationTile eventTile)
            {
                eventTile.SetRandomEvent(mapContents);

                tileType = eventTile;
            }

            return mapGrid.TryAdd((position.x, position.y), tileType);
        }

        private void DrawTilePaths(Dictionary<(int, int), ExplorationTile> mapGrid, (int x, int y) sourcePosition)
        {

            DrawPaths(-1, mapGrid, sourcePosition);
            DrawPaths(1, mapGrid, sourcePosition);
        }


        private void DrawPaths(int direction, Dictionary<(int, int), ExplorationTile> mapGrid, (int x, int y) sourcePosition)
        {
            if (mapGrid.TryGetValue((sourcePosition.x + direction, sourcePosition.y), out ExplorationTile pathableTile))
            {
                ConnectTiles(direction, pathableTile, sourcePosition, mapGrid);
            }

            if (mapGrid.TryGetValue((sourcePosition.x + direction, sourcePosition.y + 1), out pathableTile)) // Far left
            {
                ConnectTiles(direction, pathableTile, sourcePosition, mapGrid);

                if (sourcePosition.y == 0)
                {
                    return;
                }
            }

            if (mapGrid.TryGetValue((sourcePosition.x + direction, sourcePosition.y - 1), out pathableTile)) // Far right
            {

                ConnectTiles(direction, pathableTile, sourcePosition, mapGrid);
                // Draw path.

                if (sourcePosition.y == mapSize.y - 1)
                {
                    return;
                }
            }
        }

        private void ConnectTiles(int direction, ExplorationTile pathableTile, (int, int) sourceTilePosition, Dictionary<(int, int), ExplorationTile> mapGrid)
        {
            if (pathableTile == null)
            {
                return;
            }

            if (direction == -1)
            {
                pathableTile.AddConnectedTile(mapGrid[sourceTilePosition]);
            }
            else if (direction == 1)
            {
                mapGrid[sourceTilePosition].AddConnectedTile(pathableTile);
            }
        }
    }
}