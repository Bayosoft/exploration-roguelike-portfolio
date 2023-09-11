using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Card
{
    public class CardPrinter : ScriptableObject
    {
        public GameObject cardPrefab;

        public IEnumerable<CardView> PrintStackFromAbilities(IEnumerable<GameplayAbility> abilities)
        {
            List<CardView> cards = new(); // Logic of the cards

            foreach (var ability in abilities.Cast<IPlayableCard>())
            {
                var cardView = Instantiate(cardPrefab);

                var cardComponent = cardView.GetComponent<CardView>();
                cardComponent.Initialize(ability);

                cards.Add(cardComponent);
            }

            return cards;
        }
    }
}
