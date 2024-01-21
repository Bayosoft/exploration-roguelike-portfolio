using ExplorationRoguelike.Characters;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

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

        private Dictionary<(int, int), ExplorationTile> mapGrid; // row = row, column = column
        public void GenerateMap()
        {
            mapGrid = new Dictionary<(int, int), ExplorationTile>();

            int startingTiles = Random.Range(1, maxStartingTiles);
            int startingTileCount = 0;

            (int, int) sourcePosition = default;

            // TODO: Refactor to create full paths based on the starting paths (generate a full path in one go, then next path)
            for (int row = 0; row <= mapSize.x; row++)
            {
                if (row == 0 && startingTileCount < startingTiles)
                {
                    sourcePosition = SpawnStartingTile();
                    startingTileCount++;
                    continue;
                }

                List<(int,int)> viablePositions = new();

                for (int column = 0; column <= mapSize.y; column++)
                {
                    if (HasPathToSource(row, column, sourcePosition))
                    {
                        viablePositions.Add((row, column));
                    }
                }

                (int, int) randomViablePosition = viablePositions[Random.Range(0, viablePositions.Count)];

                sourcePosition = SpawnCombatTile(WeakEnemyTilePrefab, randomViablePosition);

                DrawPath(sourcePosition);
            }
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

        private (int,int) SpawnStartingTile()
        {
            int y = Random.Range(0, mapSize.y);

            return SpawnCombatTile(WeakEnemyTilePrefab, (0, y));
        }

        private (int, int) SpawnCombatTile(GameObject tile, (int x, int y) position)
        {
            var tileObject = Instantiate(tile);
            tileObject.transform.parent = this.transform;

            tileObject.transform.position = new Vector2(position.y * 100, position.x * 100);

            // TODO: Randomly select from list of enemies
            var combatTile = tileObject.GetComponent<CombatExplorationTile>();

            combatTile.EnemyData = WeakEnemies;

            mapGrid.TryAdd((position.x, position.y), combatTile);

            return (position.x, position.y);
        }

        private void DrawPath((int x, int y) sourcePosition)
        {
            int prevRow = sourcePosition.x - 1;

            if (mapGrid.TryGetValue((prevRow, sourcePosition.y), out ExplorationTile pathableTile))
            {
                // Draw path.
            }

            if (sourcePosition.y == 0 && mapGrid.GetValueOrDefault((prevRow, sourcePosition.y + 1)) != null) // Far left
            {
                // Draw path.
            }

            if (sourcePosition.y == mapSize.y - 1 && mapGrid.GetValueOrDefault((prevRow, sourcePosition.y - 1)) != null) // Far right
            {
                // Draw path.
            }
        }

    }
}