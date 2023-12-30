using ExplorationRoguelike.Characters;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ExplorationTileSpawner : MonoBehaviour
    {
        public GameObject WeakEnemyTilePrefab, AverageEnemyTilePrefab, BossEnemyTilePrefab, 
            EventTilePrefab, TreasureTilePrefab;

        public List<CharacterData> WeakEnemies; // TODO: this kind of list should be passed into GenerateMap as part of a scriptable object containing the map's data.
        public void GenerateMap()
        {           
            var weakTile = Instantiate(WeakEnemyTilePrefab);

            // TODO: Randomly select from list of enemies
            weakTile.GetComponent<CombatExplorationTile>().EnemyData = WeakEnemies;

            weakTile.transform.parent = this.transform;
        }
    }
}
