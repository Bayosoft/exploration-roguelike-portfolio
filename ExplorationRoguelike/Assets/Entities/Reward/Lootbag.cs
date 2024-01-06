using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ExplorationRoguelike.CurrencyItem;

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

            var currencyItem = lootInstance.GetComponent<CurrencyItem>();

            currencyItem.Initialize(gold, CurrencyType.Gold, null);
        }
    }
}
