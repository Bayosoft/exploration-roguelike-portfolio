using ExplorationRoguelike.GameplayTags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public interface IIntentfulAbility
    {
        public GameplayTag IntentTag { get; }
    }
}
