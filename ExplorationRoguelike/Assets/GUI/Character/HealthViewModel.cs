using ExplortationRoguelike.GUI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class HealthViewModel : ViewModel
    {
        public HealthComponent HealthComponent;

        public TextMeshProUGUI CurrentHealthText;
        public TextMeshProUGUI MaxHealthText;
        public void Initialize(HealthComponent healthComponent)
        {
            HealthComponent = healthComponent;

            HealthComponent.OnHealthChanged += UpdateHealth;
            MaxHealthText.text = $"/{HealthComponent.MaxHealth}";
            CurrentHealthText.text = Mathf.CeilToInt(HealthComponent.MaxHealth).ToString();
        }

        public void UpdateHealth(object sender, float newHealth)
        {
            if(HealthComponent != (HealthComponent)sender)
            {
                return;
            }

            CurrentHealthText.text = Mathf.CeilToInt(newHealth).ToString();
        }
    }
}
