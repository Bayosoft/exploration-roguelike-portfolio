using ExplorationRoguelike.Characters.PlayerCharacter.Events;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class PlayerHealthComponent : HealthComponent
    {
        public ScriptableEvent onPlayerDeathEvent;

        public override void Awake()
        {
            var health = GetComponent<Character>().CharacterData.Health;
            maxHealth = health.maxHealth;
            CurrentHealth = ((PlayerHealthData)health).currentHealth;
        }
        public override void OnDeath()
        {
            var onPlayerDeathEventArgs = new OnPlayerDeathEventArgs();
            onPlayerDeathEvent.RaiseEvent(onPlayerDeathEventArgs);
        }
    }
}
