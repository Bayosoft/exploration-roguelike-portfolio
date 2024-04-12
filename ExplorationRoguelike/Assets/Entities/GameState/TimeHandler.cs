using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class TimeHandler : MonoBehaviour
    {
        public static TimeHandler Instance { get; private set; }

        public static int HoursSinceStart { get; private set; }

        public static int DaysSinceStart => Mathf.FloorToInt(HoursSinceStart / 24);

        public static event EventHandler<TimeEventArgs> TimeChanged;

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            // HourSinceStart = load functionality
        }

        public static void AdvanceTime(int hours)
        {
            var timeTickEvent = new TimeTickEventArgs(hours);
            HoursSinceStart += timeTickEvent.AmountOfTimeHours;

            TimeChanged?.Invoke(Instance, timeTickEvent);
        }
    }
}
