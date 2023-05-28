using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class CardDeckComponent
    {
        public List<AbilitySO> CardsInDeck;
        public List<AbilitySO> CardsDrawn;
        public List<AbilitySO> CardsDiscarded;
        public List<AbilitySO> CardsShattered;

        public CardDeckComponent(List<AbilitySO> abilities)
        {
            CardsDrawn = abilities;
        }
    }
}
