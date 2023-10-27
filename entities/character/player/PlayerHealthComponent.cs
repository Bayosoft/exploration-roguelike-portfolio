using ExplorationRoguelike.Characters.PlayerCharacter.Events;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public partial class PlayerHealthComponent : HealthComponent
    {
        public EventResource onPlayerDeathEvent;

        // TODO: Refactor to Godot.
/*        public override void Awake()
        {
            var health = GetComponent<Character>().CharacterResource.Health;
            MaxHealth = health.MaxHealth;
            CurrentHealth = ((PlayerHealthResource)health).currentHealth;
        }*/
        public override void OnDeath()
        {
            var onPlayerDeathEventArgs = new OnPlayerDeathEventArgs();
            onPlayerDeathEvent.RaiseEvent(onPlayerDeathEventArgs);
        }
    }
}
