using ExplorationRoguelike.GUI.Card;

namespace ExplorationRoguelike.Combat.Events
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
