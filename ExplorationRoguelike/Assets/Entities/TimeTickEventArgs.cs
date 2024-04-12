namespace ExplorationRoguelike
{   
    public class TimeTickEventArgs : TimeEventArgs
    {
        public int AmountOfTimeHours;

        public TimeTickEventArgs(int amountOfTimeHours)
        {
            AmountOfTimeHours = amountOfTimeHours;
        }
    }
}
