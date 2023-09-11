using ExplorationRoguelike.GUI.Card;

namespace ExplorationRoguelike.Combat.Events
{
    public class TryPlayCardEventArgs : ConcreteEventArgs
    {
        public CardView CardView { get; }

        public TryPlayCardEventArgs(CardView cardView)
        {
            CardView = cardView;
        }

    }
}
