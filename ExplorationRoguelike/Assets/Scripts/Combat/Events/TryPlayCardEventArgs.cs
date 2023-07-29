using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class TryPlayCardEventArgs : ConcreteEventArgs
    {
        public CardAbility Card { get; }

        public TryPlayCardEventArgs(CardAbility card)
        {
            Card = card;
        }

    }
}
