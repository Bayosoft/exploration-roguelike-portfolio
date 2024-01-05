using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class Lootbag : MonoBehaviour
    {
        [SerializeField]
        private GameObject lootItem;

        public void Initialize(LootTable loot)
        {
            int gold = loot.GetRandomGoldReward();
            var lootInstance = Instantiate(lootItem);

            lootInstance.transform.SetParent(this.transform);
            lootInstance.GetComponent<LootItem>().Initialize($"{gold} Gold", null);

        }
    }
}
