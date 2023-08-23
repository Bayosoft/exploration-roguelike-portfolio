using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Card
{
    [CreateAssetMenu(fileName = "Card Printer", menuName = "Utility/Card Printer")]
    public class CardPrinter : ScriptableObject
    {
        public GameObject cardPrefab;

        public List<Card> PrintStackFromAbilities(List<GameplayAbility> abilities)
        {
            List<Card> cards = new(); // Logic of the cards

            foreach(CardAbility ability in abilities.Cast<CardAbility>())
            {
                GameObject cardView = Instantiate(cardPrefab);

                Card cardComponent = cardView.GetComponent<Card>();
                cardComponent.Initialize(ability);

                cards.Add(cardComponent);
            }

            return cards;
        }
    }
}
