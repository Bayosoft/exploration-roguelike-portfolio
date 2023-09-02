using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;

namespace ExplorationRoguelike.Combat.Events
{
    public class PlayCardEventArgs : ConcreteEventArgs
    {
        public GameplayAbility Ability { get; private set; }
        public AbilitySystemComponent Target { get; private set; }
        public AbilitySystemComponent Attacker { get; private set; }

        public PlayCardEventArgs(GameplayAbility ability, AbilitySystemComponent target, AbilitySystemComponent attacker)
        {
            Target = target;
            Attacker = attacker;
            Ability = ability;
        }
    }
}
