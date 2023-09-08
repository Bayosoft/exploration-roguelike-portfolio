using System;
using System.Collections;
using System.Collections.Generic;
using ExplorationRoguelike.Characters.PlayerCharacter;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    public class HealthComponent : MonoBehaviour
    {
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

        public virtual void Awake()
        {
            var health = GetComponent<Character>().CharacterData.Health;
            maxHealth = health.maxHealth;
            _currentHealth = health.maxHealth;
        }
        
        public void ReduceHealthBy(float amount)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        }

        public virtual void OnDeath()
        {
            var character = GetComponent<Character>();

            var characterName = (character ? character.CharacterData.Name : gameObject.name);
            Debug.Log($"{characterName} died.");
            // die
        }
    }
}
