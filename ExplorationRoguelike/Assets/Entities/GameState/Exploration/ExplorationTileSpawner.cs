using ExplorationRoguelike.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ExplorationTileSpawner : MonoBehaviour
    {
        public GameObject basicEnemyTilePrefab, AverageEnemyTilePrefab, BossEnemyTilePrefab,
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

            int startingTiles = Random.Range(1, maxStartingTiles);
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

                (int, int) randomViablePosition = viablePositions[Random.Range(0, viablePositions.Count)];

                sourcePosition = SpawnCombatTile(mapGrid, basicEnemyTilePrefab, randomViablePosition);

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
            int y = Random.Range(0, mapSize.y);

            return SpawnCombatTile(mapGrid, basicEnemyTilePrefab, (0, y));
        }

        private (int, int) SpawnCombatTile(Dictionary<(int, int), ExplorationTile> mapGrid, GameObject tile, (int x, int y) position)
        {
            var tileObject = Instantiate(tile);
            tileObject.transform.SetParent(this.transform, false);

            tileObject.transform.position = new Vector2(position.y * 100, position.x * 100);

            // TODO: Randomly select from list of enemies
            var combatTile = tileObject.GetComponent<CombatExplorationTile>();

            combatTile.EnemyData = GetRandomEnemy(mapContents.BasicEnemyPool);

            mapGrid.TryAdd((position.x, position.y), combatTile);

            return (position.x, position.y);
        }

        private CharacterData GetRandomEnemy(List<CharacterData> enemies)
        {
           return enemies[Random.Range(0, enemies.Count)];
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

                if(sourcePosition.y == mapSize.y - 1)
                {
                    return;
                }
            }
        }

    }
}