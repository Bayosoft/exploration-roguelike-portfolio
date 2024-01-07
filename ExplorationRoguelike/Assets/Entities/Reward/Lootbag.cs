using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ExplorationRoguelike.CurrencyItem;

namespace ExplorationRoguelike
{
    public class LootBag : MonoBehaviour
    {
        [SerializeField]
        private GameObject currencyPrefab;

        public Button nextButton;

        public void Initialize(LootTable loot)
        {
            int gold = loot.GetRandomGoldReward();

            if(gold > 0)
            {
                var currencyInstance = Instantiate(currencyPrefab);

                currencyInstance.transform.SetParent(this.transform);

                var currencyItem = currencyInstance.GetComponent<CurrencyItem>();

                currencyItem.Initialize(gold, CurrencyType.Gold, null);
            }
        }
    }
}
