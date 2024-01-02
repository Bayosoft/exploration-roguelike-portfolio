using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.GameplayEffects;
using System;
using System.Collections.Generic;

namespace ExplorationRoguelike
{
    public interface IAbilityEntity
    {
        public IAbilityData AbilityData { get; }
    }

    public interface IAbilityData
    {
        public List<GameplayAbility> Abilities { get; }

        public ActiveGameplayEffectContainer ActiveGameplayEffects { get; set; }
        public event EventHandler<AbilityGrantedEventArgs> AbilityGranted;
    }
}
