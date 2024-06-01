using ExplorationRoguelike.Characters.PlayerCharacter;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class PlayerArtifactDisplayer : MonoBehaviour
    {
        // [SerializeField]
        // private List<GameObject> artifactPrefab
        public void Initialize(Player player)
        {
            UpdateArtifacts(null, player.ArtifactComponent.GetArtifacts());
            player.ArtifactComponent.OnArtifactsChanged += UpdateArtifacts;
        }

        public void UpdateArtifacts(object e, int amount)
        {
            // List stuff
        }
    }
}
