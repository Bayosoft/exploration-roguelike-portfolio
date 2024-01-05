using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "LootTable")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private int minGold, maxGold;

        public int GetRandomGoldReward()
        {
            return Random.Range(minGold, maxGold);
        }
    }
}