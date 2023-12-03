namespace ExplorationRoguelike
{
    // Move to a global time calculator class.
    public enum TimeType
    {
        Minutes,
        Hours,
        Days
    }
    
    public partial class TimeTickEventArgs : TimeEventArgs
    {
        public int AmountOfTime;
        public TimeType TimeType;

        public TimeTickEventArgs(int amountOfTime, TimeType timeType)
        {
            AmountOfTime = amountOfTime;
            TimeType = timeType;
        }
    }
}
