using UnityEngine;

namespace ExplorationRoguelike
{
    public class ArtifactItem : ObtainableItem
    {

        public void Initialize(Artifact artifact, Sprite sprite)
        {
            ItemName.text = artifact.ArtifactName;
            Image.sprite = sprite;
        }
        public override void OnObtain()
        {
            onObtainEvent.RaiseEvent(new OnObtainCurrencyRewardEventArgs(_amount, _currencyType));

            Destroy(gameObject);
        }
    }
}
