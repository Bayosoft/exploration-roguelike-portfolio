using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    //  A container with accessors specific to active gameplay effects, such as telling it to remove effects by application of a given effect
    public class ActiveGameplayEffectContainer
    {
        public List<ActiveGameplayEffect> ActiveGameplayEffects { get; private set; }

    }
}
