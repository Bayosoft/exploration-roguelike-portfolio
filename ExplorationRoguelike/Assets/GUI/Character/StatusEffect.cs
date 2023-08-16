using ExplortationRoguelike.GUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExplorationRoguelike
{
    public class StatusEffect : ViewModel, IPointerEnterHandler, IPointerExitHandler
    {
        public GameplayEffect GameplayEffect;

        [SerializeField]
        private TextMeshProUGUI _effectNameText;
        [SerializeField]
        private TextMeshProUGUI _effectDescriptionText;
        public void Initialize(GameplayEffect effect)
        {
            GameplayEffect = effect;
            _effectNameText.text = effect.Name;
            _effectDescriptionText.text = effect.Description;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _effectDescriptionText.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _effectDescriptionText.gameObject.SetActive(false);
        }
    }
}
