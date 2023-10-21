namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public partial class PlayerHealthData : HealthData
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
