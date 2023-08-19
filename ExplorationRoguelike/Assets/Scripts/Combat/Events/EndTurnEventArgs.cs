using ExplorationRoguelike.Scripts;

namespace ExplorationRoguelike.Combat.Events
{
    public class EndTurnEventArgs : ConcreteEventArgs
    {
        public TurnComponent Initiator { get; }
        public EndTurnEventArgs(TurnComponent initiator)
        {
            Initiator = initiator;
        }

    }
}
