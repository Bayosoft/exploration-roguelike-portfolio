using System;

namespace ExplorationRoguelike.AbilitySystem.Abilities
{
    public interface IPlayableCard
    {
        public int ManaCost { get; }
        public GameplayAbility GameplayAbility => GetGameplayAbility<GameplayAbility>();
        public T GetGameplayAbility<T>()
        {
            if (this is not T gameplayAbility)
            {
                throw new InvalidCastException("Playable card is not an ability.");
            }
            return gameplayAbility;
        }
    }
}
