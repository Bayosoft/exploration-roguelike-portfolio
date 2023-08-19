using ExplorationRoguelike.Characters;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Character
{
    public class HealthViewModel : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        public TextMeshProUGUI CurrentHealthText;
        public TextMeshProUGUI MaxHealthText;
        public void Initialize(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;

            _healthComponent.OnHealthChanged += UpdateHealth;
            MaxHealthText.text = $"/{_healthComponent.MaxHealth}";
            CurrentHealthText.text = Mathf.CeilToInt(_healthComponent.MaxHealth).ToString();
        }

        public void UpdateHealth(object sender, float newHealth)
        {
            if(_healthComponent != (HealthComponent)sender)
            {
                return;
            }

            CurrentHealthText.text = Mathf.CeilToInt(newHealth).ToString();
        }
    }
}
