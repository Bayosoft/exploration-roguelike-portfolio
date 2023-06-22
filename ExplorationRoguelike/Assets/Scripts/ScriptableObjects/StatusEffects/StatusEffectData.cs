using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class StatusEffectData : ScriptableObject
    {
        public // TODO: Add Containers for each type of tags a status effect can have: AssetTags, GrantedTags, Required Tags, Blocking Tags, RemoveEffectsWithTags
        public abstract void Apply(AbilitySystemComponent target);
    }
}
