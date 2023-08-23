using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.AbilitySystem.CardSystem;
using ExplorationRoguelike.Combat.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Combat
{
    public class PlayerTurnComponent : TurnComponent
    {
        [SerializeField]
        private CardDeckComponent cardDeckComponent;
        public CardDeckComponent CardDeckComponent { get => cardDeckComponent; }

        public override void StartTurn()
        {
            CardDeckComponent.DrawCards(4);
            MyTurn = true;
        }

        public override void Act(GameplayAbility action, List<AbilitySystemComponent> targets)
        {
/*            if (MyTurn && action is CardAbility card)
            {
                CardDeckComponent.PlayCard(card, targets);
            }*/
        }
        public override void EndTurn()
        {
            MyTurn = false;
            CardDeckComponent.DiscardCards(CardDeckComponent.CardsDrawn.Count);
            CardDeckComponent.RefreshMana(CardDeckComponent.MaxMana);
            endTurnEvent.RaiseEvent(new EndTurnEventArgs(this));
        }
    }
}
