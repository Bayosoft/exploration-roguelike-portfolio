using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilityFiredEventArgs : ConcreteEventArgs
    {
        public Character Instigator { get; }

        public List<Character> Targets { get; }

        public Ability Ability { get; }

        public AbilityFiredEventArgs(Character instigator, List<Character> targets, Ability ability)
        {
            Instigator = instigator;
            Targets = targets;
            Ability = ability;
        }
    }
}
