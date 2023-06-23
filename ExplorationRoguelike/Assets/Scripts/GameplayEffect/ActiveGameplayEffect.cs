using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    //  Holds data directly tied to the effect being applied, including ActiveGameplayEffectHandle, GameplayEffectSpec and its start or end time, etc.
    public class ActiveGameplayEffect
    {
        public ActiveGameplayEffectHandle GameplayEffectHandle { get; private set; }
        public GameplayEffectSpecification Specification { get; private set; }
        public int StartTurn { get; private set; }
        public int EndTurn { get; private set; }
    }
}
