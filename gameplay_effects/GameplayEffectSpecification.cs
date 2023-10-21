using System.Collections;
using System.Collections.Generic;
using ExplorationRoguelike.AbilitySystem;

namespace ExplorationRoguelike.GameplayEffects
{
    // Holds data about the effect that is applied, like the GameplayEffect, stacks, duration, isDurationLocked, GameplayEffectContext.
    public class GameplayEffectSpecification 
    {
        public GameplayEffect EffectSo { get; private set; }
        public int Stacks { get; set; }
        public int Duration { get; set; }
        public bool IsDurationLocked { get; set; }
        // public int Level { get; set; }
        public AbilitySystemComponent Instigator { get; private set; }

        public List<GameplayModifierSpec> Modifiers { get; }

        public GameplayEffectSpecification(GameplayEffect effect, AbilitySystemComponent instigator /*, int level = 1*/)
        {
            EffectSo = effect;

            Duration = effect.duration;

            Instigator = instigator;
            // Level = level;

            Modifiers = new List<GameplayModifierSpec>();

            foreach(var modifier in effect.modifiers)
            {
                Modifiers.Add(new GameplayModifierSpec(modifier, instigator));
            }
        }
    }
}
