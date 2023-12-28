using ExplorationRoguelike.GUI.Card;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.CardSystem
{
    public class CardDeckComponent : MonoBehaviour
    {
        public CardPrinter printer;
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
        private AbilitySystemComponent _player;


        public void Awake()
        {
            _player = GetComponent<AbilitySystemComponent>();

            IEnumerable<Card> printedCards = printer.PrintStackFromAbilities(_player.GrantedAbilities);
            CardsInDeck  = new ObservableCollection<Card>(printedCards);
        }

        public void Start()
        {
            Mana = MaxMana;
        }

        public void DrawCards(int amountOfCards)
        {
            Card cardView;
            // TODO: Probably should loop through in case individual cards trigger abilities as they are drawn.

            for (int amountDrawn = 0; amountDrawn < amountOfCards;)
            {
                if(CardsInDeck.Count == 0)
                {
                   var shuffled = TryShuffle();

                    if (!shuffled)
                    {
                        return;
                    }
                }

                cardView = CardsInDeck.First();

                CardsInDeck.Remove(cardView);
                CardsDrawn.Add(cardView);

                ++amountDrawn;
            }
        }
        public void PlayCard(Card cardView, IEnumerable<AbilitySystemComponent> targets)
        {
            if (Mana < cardView.playableCard.ManaCost)
            {
                return;
            }
            
            var successfullyPlayed = _player.TryActivateAbility(cardView.playableCard.GameplayAbility, targets);

            if (!successfullyPlayed)
            {
                return;
            }
            
            Mana -= cardView.playableCard.ManaCost; // replace with event on IPlayableCard that fires on Activate entry.
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
            if(CardsDiscarded.Count == 0)
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
