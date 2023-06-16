using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class AbilityPartData : ScriptableObject
    {
        public int Order;

        public abstract void Activate(AbilityComponent instigator, IEnumerable<Character> targets);
        public abstract void Activate(AbilityComponent instigator, Character target);
    }
}
