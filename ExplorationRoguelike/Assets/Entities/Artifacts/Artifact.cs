using ExplorationRoguelike.GameplayEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
}