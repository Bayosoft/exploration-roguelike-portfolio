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
            List<Card> cards = new();

            foreach (var ability in abilities.Cast<IPlayableCard>())
            {
                var cardObject = Instantiate(cardPrefab);

                var card = cardObject.GetComponent<Card>();
                card.Initialize(ability);

                cards.Add(card);
            }

            return cards;
        }
    }
}
