using ExplorationRoguelike.GUI.Card;
using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExplorationRoguelike.AbilitySystem.CardSystem
{
    public partial class CardDeckComponent : Node
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

        public ObservableCollection<CardView> CardsInDeck = new();
        public ObservableCollection<CardView> CardsDrawn = new();
        public ObservableCollection<CardView> CardsDiscarded = new();
        public ObservableCollection<CardView> CardsShattered = new();
        private AbilitySystemComponent _player;

        // TODO: Refactor to Godot.
/*        public void Awake()
        {
            _player = GetComponent<AbilitySystemComponent>();

            CardsInDeck.AddRange(printer.PrintStackFromAbilities(_player.GrantedAbilities));
        }
*/
        public void Start()
        {
            Mana = MaxMana;
        }

        public void DrawCards(int amountOfCards)
        {
            CardView cardView;
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
        public void PlayCard(CardView cardView, IEnumerable<AbilitySystemComponent> targets)
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
        public void DiscardCard(CardView cardView)
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
                CardView cardView = CardsDrawn[0];
                CardsDrawn.Remove(cardView);
                CardsDiscarded.Add(cardView);
            }
        }

        /// <summary>
        /// Discard specific cards in hand.
        /// </summary>
        /// <param name="cards"></param>
        public void DiscardCards(List<CardView> cards)
        {
            foreach (CardView card in cards)
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

            foreach (CardView card in CardsDiscarded.ToList())
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
