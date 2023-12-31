using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public interface IAbilityEntity
    {
        public List<GameplayAbility> Abilities { get; }
    }
}
