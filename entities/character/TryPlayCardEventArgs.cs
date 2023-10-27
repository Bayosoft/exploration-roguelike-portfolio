using ExplorationRoguelike.GUI.PlayableCard;

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
