using ExplorationRoguelike.Characters.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class CurrentTimeDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timeText;

        public void Start()
        {
            TimeHandler.TimeChanged += UpdateTime;
        }

        public void UpdateTime(object sender, TimeEventArgs timeEvent)
        {
            timeText.text = $"Day {TimeHandler.DaysSinceStart}, Hour {TimeHandler.HoursSinceStart}";
        }
    }
}
