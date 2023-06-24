using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    // Holds data about the effect that is applied, like the GameplayEffect, stacks, duration, isDurationLocked, GameplayEffectContext.
    public class GameplayEffectSpecification 
    {
        public GameplayEffect GameplayEffect { get; private set; }
        public int Stacks { get; set; }
        public int Duration { get; set; }
        public bool IsDurationLocked { get; set; }
        public GameplayEffectContext Context { get; private set; }

        public GameplayEffectSpecification(GameplayEffect effect, GameplayEffectContext context)
        {
            GameplayEffect = effect;
            Context = context;

        }
    }
}
