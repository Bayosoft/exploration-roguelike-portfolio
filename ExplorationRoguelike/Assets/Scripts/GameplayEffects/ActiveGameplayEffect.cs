using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.GameplayEffects
{
    //  Holds data directly tied to the effect being applied, including ActiveGameplayEffectHandle, GameplayEffectSpec and its start or end time, etc.
    public class ActiveGameplayEffect
    {
        public ActiveGameplayEffect(GameplayEffectSpecification effectSpec) 
        {
            Handle = ActiveGameplayEffectHandle.GenerateNew();
            Specification = effectSpec;
            IsInhibited = false;
        }

        public ActiveGameplayEffectHandle Handle { get; private set; }
        public GameplayEffectSpecification Specification { get; private set; }
        public int RemainingTurns { get; private set; }
        public bool IsInhibited { get; private set; }
    }
}
