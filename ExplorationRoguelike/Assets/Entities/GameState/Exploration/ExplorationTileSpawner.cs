using ExplorationRoguelike.Characters;
using NUnit;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ExplorationTileSpawner : MonoBehaviour
    {
        public GameObject basicEnemyTilePrefab, eliteEnemyTilePrefab, BossEnemyTilePrefab,
            EventTilePrefab, TreasureTilePrefab, RestSiteTilePrefab;

        private MapContents mapContents;

        [SerializeField]
        private int maxStartingTiles;

        [SerializeField]
        private int maxPaths;

        [SerializeField]
        private Vector2Int mapSize;

        [SerializeField]
        private List<int> restSiteRows;

        public Dictionary<(int, int), ExplorationTile> GenerateMap(MapContents contents)
        {
            var mapGrid = new Dictionary<(int, int), ExplorationTile>();

            contents.EventLoot.ForEach(el => el.ResetPool());
            mapContents = contents;


            int startingTiles = UnityEngine.Random.Range(1, maxStartingTiles);

            (int, int) sourcePosition = default;

            for (int paths = 0; paths <= startingTiles; paths++)
            {
                for (int row = 0; row <= mapSize.x; row++)
                {
                    // If row is 0 spawn start tile
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

                    if(viablePositions.Count == 0)
                    {
                        continue;
                    }

                    (int, int) tilePosition = viablePositions[UnityEngine.Random.Range(0, viablePositions.Count)];

                    ExplorationTile tile = null;

                    if(restSiteRows.Contains(row))
                    {
                        tile = SpawnTile(mapGrid, RestSiteTilePrefab, tilePosition);
                    }
                    else if (row == mapSize.x)
                    {
                        tile = SpawnTile(mapGrid, BossEnemyTilePrefab, tilePosition);
                    }
                    else
                    {
                        var tilePrefab = DetermineTileType(tilePosition);

                        tile = SpawnTile(mapGrid, tilePrefab, tilePosition);
                    }
                        
                    if(tile != null)
                    {
                        tile.SetIsFinal(row == mapSize.x);
                    }
                    else
                    {
                        Debug.LogError($"Failed to create new tile on {tilePosition}!");
                    }

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
            int basicEnemyWeight = 4;
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

        private ExplorationTile SpawnTile(Dictionary<(int, int), ExplorationTile> mapGrid, GameObject tile, (int x, int y) position)
        {
            var tileObject = Instantiate(tile);
            tileObject.transform.SetParent(this.transform, false);

            tileObject.transform.position = new Vector2(position.y * 100, position.x * 100);

            var tileInstance = tileObject.GetComponent<ExplorationTile>();

            tileInstance.OnSpawn(mapContents);

            switch (tileInstance)
            {
                case CombatExplorationTile combatTile:
                    tileInstance = combatTile;
                    break;
                case RandomEventExplorationTile eventTile:
                    tileInstance = eventTile;
                    break;
                case DialogueExplorationTile dialogueTile:
                    tileInstance = dialogueTile;
                    break;
                default:
                    break;
            }

            bool success = mapGrid.TryAdd((position.x, position.y), tileInstance);

            return success ? tileInstance : null;
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