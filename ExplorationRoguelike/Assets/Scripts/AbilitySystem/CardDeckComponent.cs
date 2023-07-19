using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class CardDeckComponent : MonoBehaviour
    {
        public int MaxMana { get; private set; }
        public int Mana { get; private set; } = 4;
        public ObservableCollection<GameplayAbility> CardsInDeck;
        public ObservableCollection<GameplayAbility> CardsDrawn;
        public ObservableCollection<GameplayAbility> CardsDiscarded;
        public ObservableCollection<GameplayAbility> CardsShattered;
        private AbilitySystemComponent _player;

        public void Awake()
        {
            _player = GetComponent<AbilitySystemComponent>();
            CardsInDeck = new ObservableCollection<GameplayAbility>(_player.GrantedAbilities);
            CardsDrawn = new ObservableCollection<GameplayAbility>();
            CardsDiscarded = new ObservableCollection<GameplayAbility>();
            CardsShattered = new ObservableCollection<GameplayAbility>();
        }

        public void DrawCards(int amountOfCards)
        {
            // TODO: Probably should loop through in case individual cards trigger abilities as they are drawn.
            List<GameplayAbility> cards = CardsInDeck.ToList().GetRange(0, amountOfCards);

            foreach(GameplayAbility card in cards)
            {
                CardsInDeck.Remove(card);
                CardsDrawn.Add(card);

                if(CardsInDeck.Count == 0)
                {
                    Shuffle();
                }
            }            
        }
        public void PlayCard(CardAbility card, List<AbilitySystemComponent> targets)
        {
            if(Mana >= card.ManaCost)
            {
                bool activated = _player.TryActivateAbility(card, targets);

                if (activated)
                {
                    Mana -= card.ManaCost;
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
                GameplayAbility card = CardsDrawn[0];
                CardsDrawn.Remove(card);
                CardsDiscarded.Add(card);
            }
        }
        
        /// <summary>
        /// Discard specific cards in hand.
        /// </summary>
        /// <param name="cards"></param>
        public void DiscardCards(List<GameplayAbility> cards) 
        {
            foreach(GameplayAbility card in cards)
            {
                CardsDrawn.Remove(card);
                CardsDiscarded.Add(card);
            }
        }

        public void Shuffle()
        {
            foreach (GameplayAbility card in CardsDiscarded)
            {
                CardsDiscarded.Remove(card);
                CardsInDeck.Add(card);
            }
        }
    }
}
