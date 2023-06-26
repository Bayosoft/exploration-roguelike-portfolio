using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class HealthComponent : MonoBehaviour
    {
        public float MaxHealth;

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
                    OnDeath();
                }
            }
        }

        public void Start()
        {
            CurrentHealth = MaxHealth;
        }
        public void ReduceHealthBy(float amount)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        }

        public virtual void OnDeath()
        {
            Character character = GetComponent<Character>();

            string characterName = (character ? character.CharacterData.Name : gameObject.name);
            Debug.Log($"{characterName} died.");
            // die
        }
    }
}
