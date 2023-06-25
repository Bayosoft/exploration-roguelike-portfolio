using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class TakeTurnEventArgs : ConcreteEventArgs
    {
        public GameplayAbility Ability { get; private set; }
        public ICombatant Target { get; private set; }
        public ICombatant Attacker { get; private set; }

        public TakeTurnEventArgs(ICombatant target, ICombatant attacker, int damage)
        {
            Target = target;
            Attacker = attacker;
            Ability = Ability;
        }
    }
}
