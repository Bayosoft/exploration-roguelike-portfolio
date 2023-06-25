using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class HealthComponent : MonoBehaviour
    {
        public int MaxHealth;

        private int _currentHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value; 
                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    OnDeath();
                }
            }
        }

        public void Start()
        {
            CurrentHealth = MaxHealth;
        }
        public void ReduceHealthBy(int amount)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        }

        public virtual void OnDeath()
        {
            Debug.Log($"{this.gameObject.name} died.");
            // die
        }
    }
}
