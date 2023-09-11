using System.Collections.Generic;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.FireBuff
{
    public class FireBuffAbility : SelfStatusEffectAbility, IPlayableCard
    {
        [field: SerializeField]
        public TargetType TargetType { get; private set; }

        [field: SerializeField]
        public int ManaCost { get; private set; }
    }
}
