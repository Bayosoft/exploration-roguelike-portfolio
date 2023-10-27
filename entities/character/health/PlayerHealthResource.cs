using Godot;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    [GlobalClass]
    public partial class PlayerHealthResource : HealthResource
    {        
        public int currentHealth;
        
        public void UpdateCurrentHealth(int newHealth)
        {
            currentHealth = newHealth;
        }
        
        public void UpdateMaxHealth(int newHealth)
        {
            MaxHealth = newHealth;
        }
    }
}
