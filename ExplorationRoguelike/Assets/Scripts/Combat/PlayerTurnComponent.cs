using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class PlayerTurnComponent : TurnComponent<CardDeckComponent>
    {
        public override void Act(GameplayAbility action, List<AbilitySystemComponent> targets)
        {
            if (MyTurn)
            {
                CombatPlayComponent.PlayCard(action, targets);
            }
        }
        public override void EndTurn()
        {
            throw new System.NotImplementedException();
        }

    }
}
