using ExplorationRoguelike.Characters;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Character
{
    public class HealthViewModel : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        public TextMeshProUGUI currentHealthText;
        public TextMeshProUGUI maxHealthText;
        public void Initialize(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;

            _healthComponent.OnHealthChanged += UpdateHealth;
            maxHealthText.text = $"/{_healthComponent.maxHealth}";
            currentHealthText.text = Mathf.CeilToInt(_healthComponent.maxHealth).ToString();
        }

        public void UpdateHealth(object sender, float newHealth)
        {
            if(_healthComponent != (HealthComponent)sender)
            {
                return;
            }

            currentHealthText.text = Mathf.CeilToInt(newHealth).ToString();
        }
    }
}
