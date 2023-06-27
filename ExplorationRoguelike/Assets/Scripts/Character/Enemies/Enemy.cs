using UnityEngine;

namespace ExplorationRoguelike
{
    public class Enemy : Character, ICombatant
    {
        [SerializeField]
        private HealthComponent _healthComponent;
        public HealthComponent HealthComponent { get => _healthComponent; }
    }
}
