using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.AbilitySystem.CardSystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Combat.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Combat
{
    public class PlayerTurnComponent : TurnComponent
    {
        [SerializeField]
        private CardDeckComponent cardDeckComponent;
        public CardDeckComponent CardDeckComponent => cardDeckComponent;

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

            var endTurnEventArgs = new EndTurnEventArgs(this);
            endTurnEvent.RaiseEvent(endTurnEventArgs);
            // TODO: Refactor so that turn component's owner is Character directly.
            Owner.GetComponent<Character>().CharacterData.ActiveGameplayEffects.OnTimeChanged(endTurnEventArgs);
        }
    }
}
