using System;
using UnityEngine;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class PlayerHealthData : HealthData
    {        
        public int currentHealth;
        
        public void UpdateCurrentHealth(int newHealth)
        {
            currentHealth = newHealth;
        }
        
        public void UpdateMaxHealth(int newHealth)
        {
            maxHealth = newHealth;
        }
    }
}
