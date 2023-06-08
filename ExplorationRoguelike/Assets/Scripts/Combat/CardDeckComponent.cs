using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExplorationRoguelike
{
    public class CardDeckComponent
    {
        public ObservableCollection<AbilitySO> CardsInDeck;
        public ObservableCollection<AbilitySO> CardsDrawn;
        public ObservableCollection<AbilitySO> CardsDiscarded;
        public ObservableCollection<AbilitySO> CardsShattered;

        public CardDeckComponent(List<AbilitySO> abilities)
        {
            CardsInDeck = new ObservableCollection<AbilitySO>(abilities);
            CardsDrawn = new ObservableCollection<AbilitySO>();
            CardsDiscarded = new ObservableCollection<AbilitySO>();
            CardsShattered = new ObservableCollection<AbilitySO>();
        }

        public void DrawCards(int amountOfCards)
        {
            // TODO: Probably should loop through in case individual cards trigger abilities as they are drawn.
            List<AbilitySO> cards = CardsInDeck.ToList().GetRange(0, amountOfCards);

            foreach(AbilitySO card in cards)
            {
                CardsInDeck.Remove(card);
                CardsDrawn.Add(card);

                if(CardsInDeck.Count == 0)
                {
                    Shuffle();
                }
            }            
        }

        /// <summary>
        /// Discard an amount of cards from the start of the hand to the end.
        /// </summary>
        /// <param name="amountOfCards"></param>
        public void DiscardCards(int amountOfCards)
        {
            for(int i = 0; i < amountOfCards; i++)
            {
                AbilitySO card = CardsDrawn[0];
                CardsDrawn.Remove(card);
                CardsDiscarded.Add(card);
            }
        }
        
        /// <summary>
        /// Discard specific cards in hand.
        /// </summary>
        /// <param name="cards"></param>
        public void DiscardCards(List<AbilitySO> cards) 
        {
            foreach(AbilitySO card in cards)
            {
                CardsDrawn.Remove(card);
                CardsDiscarded.Add(card);
            }
        }

        public void Shuffle()
        {
            foreach (AbilitySO card in CardsDiscarded)
            {
                CardsDiscarded.Remove(card);
                CardsInDeck.Add(card);
            }
        }
    }
}
