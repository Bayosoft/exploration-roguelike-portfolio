using ExplorationRoguelike.AbilitySystem.Abilities;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class MultiChoiceOption : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI multiChoiceOptionText;

        private MultiChoiceOptionData _multiChoiceData;

        public void Initialize(MultiChoiceOptionData multiChoiceData)
        {
            _multiChoiceData = multiChoiceData;

            multiChoiceOptionText.text = _multiChoiceData.OptionText;
        }
        public void OnOptionClicked()
        {
            _multiChoiceData.Activate();
        }
    }
}
