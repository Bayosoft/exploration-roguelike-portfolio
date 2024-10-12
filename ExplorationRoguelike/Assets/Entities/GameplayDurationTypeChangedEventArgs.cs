namespace ExplorationRoguelike.GameState
{
    public class GameplayDurationTypeChangedEventArgs : TimeEventArgs
    {
        public GameplayDurationType NewDurationType { get; }
        public GameplayDurationTypeChangedEventArgs(GameplayDurationType newDurationType)
        {
            NewDurationType = newDurationType;
        }

    }
}