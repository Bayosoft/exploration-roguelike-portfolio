using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Character
{
    public class HealthDisplayer : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        public TextMeshProUGUI currentHealthText;
        public TextMeshProUGUI maxHealthText;

        public void Initialize(Player player)
        {
            _healthComponent = player.HealthComponent;

            _healthComponent.OnHealthChanged += UpdateHealth;
            maxHealthText.text = $"/{_healthComponent.MaxHealth}";
            currentHealthText.text = Mathf.CeilToInt(_healthComponent.MaxHealth).ToString();
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
