using ExplorationRoguelike.GameplayEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExplorationRoguelike
{
    public class ArtifactOverlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [NonSerialized] public Artifact Artifact;

        [SerializeField]
        private TextMeshProUGUI artifactNameText;
        [SerializeField]
        private TextMeshProUGUI artifactDescriptionText;

        public void Initialize(Artifact artifact)
        {
            Artifact = artifact;
            artifactNameText.text = artifact.ArtifactName;
            artifactDescriptionText.text = artifact.description;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            artifactNameText.gameObject.SetActive(true);
            artifactDescriptionText.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            artifactNameText.gameObject.SetActive(false);
            artifactDescriptionText.gameObject.SetActive(false);
        }
    }
}
