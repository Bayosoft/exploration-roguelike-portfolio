using UnityEngine;

namespace ExplorationRoguelike
{
    public class ArtifactItem : ObtainableItem
    {
        private Artifact _artifact;

        public void Initialize(Artifact artifact)
        {
            _artifact = artifact;
            ItemName.text = artifact.ArtifactName;
            // Image.sprite = artifact.Sprite;
        }

        public override void OnObtain()
        {
            onObtainEvent.RaiseEvent(new OnObtainArtifactRewardEventArgs(_artifact));

            Destroy(gameObject);
        }
    }
}
