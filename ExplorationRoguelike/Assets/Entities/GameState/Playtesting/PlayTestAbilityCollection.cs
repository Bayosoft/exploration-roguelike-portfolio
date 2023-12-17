using System.Collections;
using System.Collections.Generic;
using ExplorationRoguelike.AbilitySystem.Abilities;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu()]
    public class PlayTestAbilityCollection : ScriptableObject
    {
        public List<GameplayAbility> abilities;
    }
}
