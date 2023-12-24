using System;
using UnityEngine;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    [Serializable]
    public class PlayerHealth 
    {
        [NonSerialized]
        public CharacterHealth characterHealth;

        public int currentHealth;

        public void UpdateCurrentHealth(int newHealth)
        {
            currentHealth = newHealth;
        }
        
        public void UpdateMaxHealth(int newHealth)
        {
            characterHealth.maxHealth = newHealth;
        }
    }
}
