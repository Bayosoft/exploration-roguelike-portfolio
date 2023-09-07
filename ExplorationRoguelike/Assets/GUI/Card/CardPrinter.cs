using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Card
{
    public class CardPrinter : ScriptableObject
    {
        public GameObject cardPrefab;

        public IEnumerable<Card> PrintStackFromAbilities(IEnumerable<GameplayAbility> abilities)
        {
            List<Card> cards = new(); // Logic of the cards

            foreach(var ability in abilities.Cast<CardAbility>())
            {
                var cardView = Instantiate(cardPrefab);

                var cardComponent = cardView.GetComponent<Card>();
                cardComponent.Initialize(ability);

                cards.Add(cardComponent);
            }
            return cards;
        }
    }
}
