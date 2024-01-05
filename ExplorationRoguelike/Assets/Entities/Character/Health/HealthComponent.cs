using System;
using System.Collections;
using System.Collections.Generic;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Characters.PlayerCharacter.Events;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    public class HealthComponent : MonoBehaviour
    {
        protected Character owner;

        protected float maxHealth;
        public float MaxHealth => maxHealth;
        public event EventHandler<float> OnHealthChanged;
        
        public ScriptableEvent onDeathEvent;

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
            owner = GetComponent<Character>();
            var health = owner.CharacterData.Health;
            maxHealth = health.maxHealth;
            _currentHealth = health.maxHealth;
        }
        
        public void ReduceHealthBy(float amount)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        }

        public virtual void OnDeath()
        {
            var characterName = (owner ? owner.CharacterData.Name : gameObject.name);
            Debug.Log($"{characterName} died.");

            var onDeathEventArgs = new OnDeathEventArgs(owner);
            onDeathEvent.RaiseEvent(onDeathEventArgs);
        }
    }
}
