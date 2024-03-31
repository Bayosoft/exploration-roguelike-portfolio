using ExplorationRoguelike.Characters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "MapContents", menuName = "MapContents")]
    public class MapContents : ScriptableObject
    {
        [SerializeField]
        private Sprite backgroundImage;

        [SerializeField]
        private List<CharacterData> basicEnemyPool, eliteEnemyPool;

        [SerializeField]
        private CharacterData bossEnemy;

        public List<CharacterData> BasicEnemyPool => basicEnemyPool;
        public List<CharacterData> EliteEnemyPool => eliteEnemyPool;
        public CharacterData BossEnemy => bossEnemy;

        [SerializeField]
        private List<LootTable> eventLoot;

        // DialogueCollection ..

        [SerializeField]
        private List<CharacterData> eventEnemies;


        public List<LootTable> EventLoot => eventLoot;
        // public DialogueCollection EventDialogue;
        public List<CharacterData> EventEnemies => eventEnemies;

        [SerializeField]
        private Dialogue restSiteDialogue;
        public Dialogue RestSiteDialogue => restSiteDialogue;
        public List<CharacterData> GetEnemyPool(EnemyTier enemyTier)
        {
            return enemyTier switch
            {
                EnemyTier.BASIC => basicEnemyPool,
                EnemyTier.ELITE => eliteEnemyPool,
                EnemyTier.BOSS => new List<CharacterData>() { bossEnemy },
                _ => throw new NotImplementedException(),
            };
        }
    }
}