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


        void Start()
        {
         //   CurrentHealth = MaxHealth;
        }

        public void Create()
        {

        }

    }
}
