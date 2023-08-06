using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class PlayerTurnComponent : TurnComponent
    {
        [SerializeField]
        private CardDeckComponent _cardDeckComponent;
        public CardDeckComponent CardDeckComponent { get => _cardDeckComponent; }

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
            EndTurnEvent.RaiseEvent(new EndTurnEventArgs(this));
        }
    }
}
