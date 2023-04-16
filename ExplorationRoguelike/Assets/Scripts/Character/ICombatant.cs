using ExplorationRoguelike;

public interface ICombatant
{
    public AbilityComponent AbilityComponent { get; }
    public HealthComponent HealthComponent { get; }
}