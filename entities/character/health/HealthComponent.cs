using System;
using Godot;

namespace ExplorationRoguelike.Characters
{
    public partial class HealthComponent : Node
    {
        [Export]
        private HealthResource healthResource;

        protected float maxHealth;
        public float MaxHealth => maxHealth;
        public event EventHandler<float> OnHealthChanged;

        private float _currentHealth;
        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value; 
                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    OnHealthChanged?.Invoke(this, _currentHealth);
                    OnDeath();
                    return;
                }
                OnHealthChanged?.Invoke(this, _currentHealth);
            }
        }
        public override void _Ready()
        {
            maxHealth = healthResource.MaxHealth;
            _currentHealth = healthResource.MaxHealth;
        }
        
        public void ReduceHealthBy(float amount)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        }

        public virtual void OnDeath()
        {
          /*  var character = GetComponent<Character>();

            var characterName = (character ? character.CharacterResource.Name : gameObject.name);
            Debug.Log($"{characterName} died.");
            // die*/
        }
    }
}
