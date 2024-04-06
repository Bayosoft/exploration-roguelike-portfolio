using System;
using ExplorationRoguelike.GameplayEffects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExplorationRoguelike.StatusEffect
{
    public class StatusEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [NonSerialized] public ActiveGameplayEffect GameplayEffect;

        [SerializeField]
        private TextMeshProUGUI effectNameText;
        [SerializeField]
        private TextMeshProUGUI effectDescriptionText;
        [SerializeField]
        private TextMeshProUGUI effectDurationText;
        [SerializeField] 
        private TextMeshProUGUI effectStacksText;
        public void Initialize(ActiveGameplayEffect effect)
        {
            GameplayEffect = effect;
            effectNameText.text = effect.Specification.EffectSo.name;
            effectDescriptionText.text = effect.Specification.EffectSo.description;
            effectDurationText.text = effect.RemainingDuration.ToString();
            effect.DurationChanged += UpdateDurationText;
            // if(effect.Stackable){ effectStacksText.Enable ... }
        }

        private void UpdateDurationText(object sender, EventArgs e)
        {
            effectDurationText.text = GameplayEffect.RemainingDuration.ToString();
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
