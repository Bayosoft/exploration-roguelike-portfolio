using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class PlayerTurnComponent : TurnComponent<CardDeckComponent>
    {
        public override void TakeAction()
        {
           // CombatPlayComponent.PlayCard();
            throw new System.NotImplementedException();
        }
        public override void EndTurn()
        {
            throw new System.NotImplementedException();
        }

    }
}
