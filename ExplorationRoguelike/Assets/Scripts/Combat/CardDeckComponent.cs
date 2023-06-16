using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExplorationRoguelike
{
    public class CardDeckComponent
    {
        public ObservableCollection<AbilityData> CardsInDeck;
        public ObservableCollection<AbilityData> CardsDrawn;
        public ObservableCollection<AbilityData> CardsDiscarded;
        public ObservableCollection<AbilityData> CardsShattered;

        public CardDeckComponent(List<AbilityData> abilities)
        {
            CardsInDeck = new ObservableCollection<AbilityData>(abilities);
            CardsDrawn = new ObservableCollection<AbilityData>();
            CardsDiscarded = new ObservableCollection<AbilityData>();
            CardsShattered = new ObservableCollection<AbilityData>();
        }

        public void DrawCards(int amountOfCards)
        {
            // TODO: Probably should loop through in case individual cards trigger abilities as they are drawn.
            List<AbilityData> cards = CardsInDeck.ToList().GetRange(0, amountOfCards);

            foreach(AbilityData card in cards)
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
                AbilityData card = CardsDrawn[0];
                CardsDrawn.Remove(card);
                CardsDiscarded.Add(card);
            }
        }
        
        /// <summary>
        /// Discard specific cards in hand.
        /// </summary>
        /// <param name="cards"></param>
        public void DiscardCards(List<AbilityData> cards) 
        {
            foreach(AbilityData card in cards)
            {
                CardsDrawn.Remove(card);
                CardsDiscarded.Add(card);
            }
        }

        public void Shuffle()
        {
            foreach (AbilityData card in CardsDiscarded)
            {
                CardsDiscarded.Remove(card);
                CardsInDeck.Add(card);
            }
        }
    }
}
