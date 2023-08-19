using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Combat;

namespace ExplorationRoguelike.Characters
{
    public interface ICombatant
    {
        public AbilitySystemComponent AbilitySystemComponent { get; }
        public HealthComponent HealthComponent { get; }
        public TurnComponent TurnComponent { get; }
    }
}
