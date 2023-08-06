using ExplorationRoguelike.GUI.Card;

namespace ExplorationRoguelike
{
    public class TryPlayCardEventArgs : ConcreteEventArgs
    {
        public Card Card { get; }

        public TryPlayCardEventArgs(Card card)
        {
            Card = card;
        }

    }
}
