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

        [SerializeField]
        private GameObject artifactPrefab;

        public Button nextButton;

        public void Initialize(LootTable loot)
        {
            (int gold, List<Artifact> artifacts) = loot.GenerateLootTable(1);

            if (gold > 0)
            {
                var currencyInstance = Instantiate(currencyPrefab);

                currencyInstance.transform.SetParent(this.transform.GetChild(0).transform);

                var currencyItem = currencyInstance.GetComponent<CurrencyItem>();

                currencyItem.Initialize(gold, CurrencyType.Gold, null);
            }

            if(artifacts.Count > 0)
            {
                foreach(Artifact artifact in artifacts)
                {                  
                    var artifactInstance = Instantiate(artifactPrefab);

                    artifactInstance.transform.SetParent(this.transform.GetChild(0).transform);

                    var artifactItem = artifactInstance.GetComponent<ArtifactItem>();

                    artifactItem.Initialize(artifact);
                }
            }
        }
    }
}
