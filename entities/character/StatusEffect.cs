using System;
using ExplorationRoguelike.GameplayEffects;
using Godot;

namespace ExplorationRoguelike.GUI.Characters
{
    public partial class StatusEffect : Node2D
    {
        [NonSerialized] public ActiveGameplayEffect GameplayEffect;

        [Export]
        private Label effectNameLabel;
        [Export]
        private Label effectDescriptionLabel;
        [Export]
        private Label effectDurationLabel;
        [Export] 
        private Label effectStacksLabel;
        public void Initialize(ActiveGameplayEffect effect)
        {
            GameplayEffect = effect;
            effectNameLabel.Text = effect.Specification.EffectResource.name;
            effectDescriptionLabel.Text = effect.Specification.EffectResource.description;
            effectDurationLabel.Text = effect.RemainingDuration.ToString();
            effect.DurationChanged += UpdateDurationText;
            // if(effect.Stackable){ effectStacksText.Enable ... }
        }

        private void UpdateDurationText(object sender, EventArgs e)
        {
            effectDurationLabel.Text = GameplayEffect.RemainingDuration.ToString();
        }

        public void OnPointerEnter()
        {
            effectDescriptionLabel.Show();
        }

        public void OnPointerExit()
        {
            effectDescriptionLabel.Hide();
        }
    }
}
