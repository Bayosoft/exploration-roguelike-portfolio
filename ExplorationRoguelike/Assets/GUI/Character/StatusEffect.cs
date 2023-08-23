using System;
using ExplorationRoguelike.GameplayEffects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExplorationRoguelike.GUI.Character
{
    public class StatusEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [NonSerialized] public GameplayEffect GameplayEffect;

        [SerializeField]
        private TextMeshProUGUI effectNameText;
        [SerializeField]
        private TextMeshProUGUI effectDescriptionText;
        public void Initialize(GameplayEffect effect)
        {
            GameplayEffect = effect;
            effectNameText.text = effect.name;
            effectDescriptionText.text = effect.description;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            effectDescriptionText.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            effectDescriptionText.gameObject.SetActive(false);
        }
    }
}
