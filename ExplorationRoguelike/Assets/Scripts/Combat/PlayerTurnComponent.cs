using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.AbilitySystem.CardSystem;
using ExplorationRoguelike.Combat.Events;
using Godot;
using System.Collections.Generic;

namespace ExplorationRoguelike.Combat
{
    public partial class PlayerTurnComponent : TurnComponent
    {
        [Export]
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
            Owner.ActiveGameplayEffects.OnTimeChanged(endTurnEventArgs);
        }
    }
}
