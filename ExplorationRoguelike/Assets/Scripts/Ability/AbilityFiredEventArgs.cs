using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilityFiredEventArgs : ConcreteEventArgs
    {
        public Character Instigator { get; }

        public List<Character> Targets { get; }

        public AbilityData Ability { get; }

        public AbilityFiredEventArgs(Character instigator, List<Character> targets, AbilityData ability)
        {
            Instigator = instigator;
            Targets = targets;
            Ability = ability;
        }
    }
}
