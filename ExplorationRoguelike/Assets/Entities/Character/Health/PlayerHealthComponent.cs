using ExplorationRoguelike.Characters.PlayerCharacter.Events;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class PlayerHealthComponent : HealthComponent
    {
        public ScriptableEvent onPlayerDeathEvent;

        public override void Awake()
        {
            var pcData = GetComponent<Player>().PlayerCharacterData;

            var playerHealth = pcData.PlayerHealth;
            playerHealth.characterHealth = pcData.Health;

            maxHealth = playerHealth.characterHealth.maxHealth;
            CurrentHealth = playerHealth.currentHealth; 
        }
        public override void OnDeath()
        {
            var onPlayerDeathEventArgs = new OnPlayerDeathEventArgs();
            onPlayerDeathEvent.RaiseEvent(onPlayerDeathEventArgs);
        }
    }
}
