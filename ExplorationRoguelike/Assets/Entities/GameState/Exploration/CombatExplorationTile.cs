using ExplorationRoguelike.Characters;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ExplorationRoguelike
{
    [Serializable]
    public enum EnemyTier
    {
        BASIC,
        ELITE,
        BOSS
    }

    public class CombatExplorationTile : ExplorationTile
    {
        [SerializeField]
        private EnemyTier enemyTier;
        public EnemyTier EnemyTier => enemyTier;

        public CharacterData Enemy { get; private set; }

        public void SetRandomEnemy(List<CharacterData> enemyPool)
        {
            if(enemyPool != null && enemyPool.Count > 0)
            {
                Enemy = enemyPool[UnityEngine.Random.Range(0, enemyPool.Count)];
            }
        }

        public override void ExploreTile()
        {
            exploreTileEvent.RaiseEvent(new ExploreTileEventArgs(this));
        }

        public override void Selected()
        {
            base.Selected();

            if(Enemy != null)
            {
                FindFirstObjectByType<GameState>().SetCombatAdditive(Enemy);
            }
            
            this.enabled = false;
        }
    }
}
