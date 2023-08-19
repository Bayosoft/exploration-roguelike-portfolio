using ExplorationRoguelike.Characters.PlayerCharacter.Events;
using ExplorationRoguelike.Scripts;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class PlayerHealthComponent : HealthComponent
    {
        public ScriptableEvent OnPlayerDeathEvent;

        public override void OnDeath()
        {
            OnPlayerDeathEventArgs onPlayerDeathEventArgs = new OnPlayerDeathEventArgs();
            OnPlayerDeathEvent.RaiseEvent(onPlayerDeathEventArgs);
        }
    }
}
