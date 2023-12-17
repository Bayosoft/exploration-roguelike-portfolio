namespace ExplorationRoguelike.Combat.Events
{
    public class CombatEventArgs : ConcreteEventArgs
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public CombatEventArgs()
        {
            
        }

        public CombatEventArgs(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}
