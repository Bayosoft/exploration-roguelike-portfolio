using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class EndTurnEventArgs : ConcreteEventArgs
    {
        public TurnComponent Initiator { get; }
        public EndTurnEventArgs(TurnComponent initiator)
        {
            Initiator = initiator;
        }

    }
}
