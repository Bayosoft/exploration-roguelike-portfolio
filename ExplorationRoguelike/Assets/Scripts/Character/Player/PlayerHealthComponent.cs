namespace ExplorationRoguelike
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
