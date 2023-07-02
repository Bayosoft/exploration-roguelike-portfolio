using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
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
