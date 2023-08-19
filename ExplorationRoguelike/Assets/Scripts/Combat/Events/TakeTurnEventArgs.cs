using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Scripts;

namespace ExplorationRoguelike.Combat.Events
{
    public class TakeTurnEventArgs : ConcreteEventArgs
    {
        public GameplayAbility Ability { get; private set; }
        public AbilitySystemComponent Target { get; private set; }
        public AbilitySystemComponent Attacker { get; private set; }

        public TakeTurnEventArgs(GameplayAbility ability, AbilitySystemComponent target, AbilitySystemComponent attacker)
        {
            Target = target;
            Attacker = attacker;
            Ability = ability;
        }
    }
}
