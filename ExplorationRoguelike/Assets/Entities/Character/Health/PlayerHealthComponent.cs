using ExplorationRoguelike.Characters.PlayerCharacter.Events;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class PlayerHealthComponent : HealthComponent
    { 
        public override void Awake()
        {
            owner = GetComponent<Player>();
            var pcData = ((Player)owner).PlayerCharacterData;

            var playerHealth = pcData.PlayerHealth;
            playerHealth.characterHealth = pcData.Health;

            maxHealth = playerHealth.characterHealth.maxHealth;
            CurrentHealth = playerHealth.currentHealth; 
        }
        public override void OnDeath()
        {
            var onPlayerDeathEventArgs = new OnDeathEventArgs(owner);
            onDeathEvent.RaiseEvent(onPlayerDeathEventArgs);
        }
    }
}
