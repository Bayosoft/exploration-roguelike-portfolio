using UnityEngine;

namespace ExplorationRoguelike
{
    public class Enemy : Character, ICombatant
    {
        [SerializeField]
        private AbilityComponent _abilityComponent;
        public AbilityComponent AbilityComponent { get => _abilityComponent; }

        [SerializeField]
        private HealthComponent _healthComponent;
        public HealthComponent HealthComponent { get => _healthComponent; }

        private int _currentHealth;


        public int CurrentHealth
        {
            get { return _currentHealth; }
            private set
            {
                _currentHealth = value;
                if (_currentHealth == 0)
                {
                    // CombatantDied?.Invoke(); 
                }
            }
        }


        void Start()
        {
         //   CurrentHealth = MaxHealth;
        }

    }
}
