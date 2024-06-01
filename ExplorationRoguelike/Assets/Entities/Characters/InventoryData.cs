using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Character/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        public int Gold { get; private set; }

        public void AddGold(int amount) => Gold += amount;
    }
}