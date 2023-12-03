using ExplorationRoguelike.GUI.PlayableCard;
using Godot;

namespace ExplorationRoguelike.Combat.Events
{
    [GlobalClass]
    public partial class TryPlayCardEventArgs : ConcreteEventArgs
    {
        public Card CardView { get; }

        public TryPlayCardEventArgs(Card card)
        {
            CardView = card;
        }

    }
}
