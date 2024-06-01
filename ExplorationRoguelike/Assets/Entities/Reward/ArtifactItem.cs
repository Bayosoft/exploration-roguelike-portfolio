using UnityEngine;
using static ExplorationRoguelike.CurrencyItem;

namespace ExplorationRoguelike
{
    public class ArtifactItem : LootItem
    {

        public void Initialize(Artifact artifact, Sprite sprite)
        {
            ItemName.text = artifact.ArtifactName;
            Image.sprite = sprite;
        }
        public override void OnClaimReward()
        {
            onClaimRewardEvent.RaiseEvent(new OnClaimCurrencyRewardEventArgs(_amount, _currencyType));

            Destroy(gameObject);
        }
    }
}
