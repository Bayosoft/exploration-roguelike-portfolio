using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Artifact", menuName = "Artifact")]
    public class Artifact : ScriptableObject
    {
        [SerializeField]
        private string artifactName;
        public string ArtifactName => artifactName;

        [SerializeField]
        private GameplayEffect gameplayEffect;
        public GameplayEffect GameplayEffect => gameplayEffect;

        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;

        public string description; // Should be of same type as card description.

        public bool InPool { get; set; } = true;
    }
}