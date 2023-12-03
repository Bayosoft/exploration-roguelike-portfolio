namespace ExplorationRoguelike.Combat.Events
{
    public partial class EndTurnEventArgs : TimeEventArgs
    {
        public TurnComponent Initiator { get; }
        public EndTurnEventArgs(TurnComponent initiator)
        {
            Initiator = initiator;
        }

    }
}
