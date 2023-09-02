namespace ExplorationRoguelike.Combat.Events
{
    public class EndTurnEventArgs : TimeEventArgs
    {
        public TurnComponent Initiator { get; }
        public EndTurnEventArgs(TurnComponent initiator)
        {
            Initiator = initiator;
        }

    }
}
