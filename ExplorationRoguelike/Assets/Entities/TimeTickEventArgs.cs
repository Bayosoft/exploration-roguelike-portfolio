namespace ExplorationRoguelike
{   
    public class TimeTickEventArgs : TimeEventArgs
    {
        public int AmountOfTimeHours { get; }

        public TimeTickEventArgs(int amountOfTimeHours)
        {
            AmountOfTimeHours = amountOfTimeHours;
        }
    }
}
