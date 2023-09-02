using ExplorationRoguelike.Characters.PlayerCharacter.Events;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class PlayerHealthComponent : HealthComponent
    {
        public ScriptableEvent onPlayerDeathEvent;

        public override void OnDeath()
        {
            OnPlayerDeathEventArgs onPlayerDeathEventArgs = new OnPlayerDeathEventArgs();
            onPlayerDeathEvent.RaiseEvent(onPlayerDeathEventArgs);
        }
    }
}
