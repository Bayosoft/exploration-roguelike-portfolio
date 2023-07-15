using ExplorationRoguelike;

public interface ICombatant
{ 
    public AbilitySystemComponent AbilitySystemComponent { get; }
    public HealthComponent HealthComponent { get; }
    public TurnComponent TurnComponent { get; }
}