using ExplorationRoguelike.GUI.Card;

namespace ExplorationRoguelike.Combat.Events
{
    public class TryPlayCardEventArgs : ConcreteEventArgs
    {
        public Card CardView { get; }

        public TryPlayCardEventArgs(Card cardView)
        {
            CardView = cardView;
        }

    }
}
