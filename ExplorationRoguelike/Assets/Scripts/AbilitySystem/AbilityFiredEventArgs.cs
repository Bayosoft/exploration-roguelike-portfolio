using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilityFiredEventArgs : ConcreteEventArgs
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
