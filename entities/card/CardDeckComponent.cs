using ExplorationRoguelike.GUI.PlayableCard;
using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExplorationRoguelike.AbilitySystem.CardSystem
{
    public partial class CardDeckComponent : Node
    {
        [Export]
        private CardPrinter printer;
        public int MaxMana { get; private set; } = 4;

        private int _mana;
        public int Mana
        {
            get { return _mana; }
            private set
            {
                _mana = value;
                OnManaChanged?.Invoke(this, _mana);
            }
        }
        public event EventHandler<int> OnManaChanged;

        public ObservableCollection<Card> CardsInDeck = new();
        public ObservableCollection<Card> CardsDrawn = new();
        public ObservableCollection<Card> CardsDiscarded = new();
        public ObservableCollection<Card> CardsShattered = new();

        [Export]
        private AbilitySystemComponent _abilityComponent;

        public override void _Ready()
        {
            Mana = MaxMana;
            IEnumerable<Card> cards = printer.PrintStackFromAbilities(_abilityComponent.GrantedAbilities);
            foreach (var card in cards)
            {
                CardsInDeck.Add(card);
                AddChild(card);
            }
        }

        public void DrawCards(int amountOfCards)
        {
            Card card;
            // TODO: Probably should loop through in case individual cards trigger abilities as they are drawn.

            for (int amountDrawn = 0; amountDrawn < amountOfCards;)
            {
                if (CardsInDeck.Count == 0)
                {
                    var shuffled = TryShuffle();

                    if (!shuffled)
                    {
                        return;
                    }
                }

                card = CardsInDeck.First();

                CardsInDeck.Remove(card);
                CardsDrawn.Add(card);

                ++amountDrawn;
            }
        }
        public void PlayCard(Card cardView, IEnumerable<AbilitySystemComponent> targets)
        {
            if (Mana < cardView.PlayableCard.ManaCost)
            {
                return;
            }

            var successfullyPlayed = _abilityComponent.TryActivateAbility(cardView.PlayableCard.GameplayAbility, targets);

            if (!successfullyPlayed)
            {
                return;
            }

            Mana -= cardView.PlayableCard.ManaCost; // replace with event on IPlayableCard that fires on Activate entry.
            DiscardCard(cardView);
        }
        /// <summary>
        /// Discard a specific card from hand.
        /// </summary>
        /// <param name="cardView"></param>
        public void DiscardCard(Card cardView)
        {
            CardsDrawn.Remove(cardView);
            CardsDiscarded.Add(cardView);
        }

        /// <summary>
        /// Discard an amount of cards from the start of the hand to the end.
        /// </summary>
        /// <param name="amountOfCards"></param>
        public void DiscardCards(int amountOfCards)
        {
            for (int i = 0; i < amountOfCards; i++)
            {
                Card cardView = CardsDrawn[0];
                CardsDrawn.Remove(cardView);
                CardsDiscarded.Add(cardView);
            }
        }

        /// <summary>
        /// Discard specific cards in hand.
        /// </summary>
        /// <param name="cards"></param>
        public void DiscardCards(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                CardsDrawn.Remove(card);
                CardsDiscarded.Add(card);
            }
        }

        public bool TryShuffle()
        {
            if (CardsDiscarded.Count == 0)
            {
                return false;
            }

            foreach (Card card in CardsDiscarded.ToList())
            {
                CardsDiscarded.Remove(card);
                CardsInDeck.Add(card);
            }
            return true;
        }

        public void RefreshMana(int amount)
        {
            if (amount + Mana >= MaxMana)
            {
                Mana = MaxMana;
                return;
            }

             Mana += amount;
        }
    }
}