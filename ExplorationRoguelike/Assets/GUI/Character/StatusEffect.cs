using System;
using ExplorationRoguelike.GameplayEffects;
using JetBrains.Annotations;
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
        [SerializeField]
        private TextMeshProUGUI effectDurationText;
        [SerializeField] 
        private TextMeshProUGUI effectStacksText;
        public void Initialize(GameplayEffect effect)
        {
            GameplayEffect = effect;
            effectNameText.text = effect.name;
            effectDescriptionText.text = effect.description;
            effectDurationText.text = effect.duration.ToString();
            
            // if(effect.Stackable){ effectStacksText.Enable ... }
        }

        public void OnTimeChanged()
        {
            
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
