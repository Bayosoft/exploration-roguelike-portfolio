using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using System.Collections.Generic;

namespace ExplorationRoguelike.AbilitySystem
{
    public partial class AbilityFiredEventArgs : ConcreteEventArgs
    {
        public Character Instigator { get; }

        public List<Character> Targets { get; }

        public GameplayAbility Ability { get; }

        public AbilityFiredEventArgs(Character instigator, List<Character> targets, GameplayAbility ability)
        {
            Instigator = instigator;
            Targets = targets;
            Ability = ability;
        }
    }
}
