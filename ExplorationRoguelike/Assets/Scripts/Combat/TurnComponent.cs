using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class TurnComponent<T>
    {
        public T CombatPlayComponent { get; private set; }
        public abstract void TakeAction();
        public abstract void EndTurn();
    }
}
