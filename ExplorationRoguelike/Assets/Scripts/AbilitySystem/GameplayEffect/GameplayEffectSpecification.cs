using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    // Holds data about the effect that is applied, like the GameplayEffect, stacks, duration, isDurationLocked, GameplayEffectContext.
    public class GameplayEffectSpecification 
    {
        public GameplayEffect EffectSO { get; private set; }
        public int Stacks { get; set; }
        public int Duration { get; set; }
        public bool IsDurationLocked { get; set; }
        public int Level { get; set; }
        public GameplayEffectContext Context { get; private set; }

        public List<GameplayModifierSpec> Modifiers { get; private set; }

        public GameplayEffectSpecification(GameplayEffect effect, GameplayEffectContext context, int level = 1)
        {
            EffectSO = effect;

            Duration = effect.Duration;

            Context = context;
            Level = level;
        }
    }
}
