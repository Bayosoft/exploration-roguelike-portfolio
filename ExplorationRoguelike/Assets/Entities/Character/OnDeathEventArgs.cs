namespace ExplorationRoguelike.Characters.PlayerCharacter.Events
{
    public class OnDeathEventArgs : ConcreteEventArgs
    {
        public Character DeadCharacter { get; }

        public OnDeathEventArgs(Character deadCharacter)
        {
            DeadCharacter = deadCharacter;
        }

    }
}
