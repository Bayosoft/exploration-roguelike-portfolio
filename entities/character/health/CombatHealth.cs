using ExplorationRoguelike.Characters;
using Godot;

namespace ExplorationRoguelike.GUI.Character
{
    public partial class CombatHealth : Node2D
    {
        private HealthComponent _healthComponent;

        [Export] private RichTextLabel currentHealthLabel;
        [Export] private RichTextLabel maxHealthLabel;
        public void Initialize(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;

            _healthComponent.OnHealthChanged += UpdateHealth;
            maxHealthLabel.Text = $"/{_healthComponent.MaxHealth}";
            currentHealthLabel.Text = Mathf.CeilToInt(_healthComponent.MaxHealth).ToString();
        }

        public void UpdateHealth(object sender, float newHealth)
        {
            if(_healthComponent != (HealthComponent)sender)
            {
                return;
            }

            currentHealthLabel.Text = Mathf.CeilToInt(newHealth).ToString();
        }
    }
}
