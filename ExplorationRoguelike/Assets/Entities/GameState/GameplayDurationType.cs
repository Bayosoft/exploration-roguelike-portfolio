using System;

namespace ExplorationRoguelike.GameState
{
    [Serializable]
    public enum GameplayDurationType
    {
        Instant,
        Turns,
        Time,
        Infinite
    }
}
