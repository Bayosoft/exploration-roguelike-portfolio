using System.Collections.Generic;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.FrostBuff
{
    [CreateAssetMenu(fileName = "FrostBuffAbility", menuName = "Abilities/FrostBuff")]
    public class FrostBuffAbility : SelfStatusEffectAbility, IPlayableCard
    {
        [field: SerializeField]
        public TargetType TargetType { get; private set; }

        [field: SerializeField]
        public int ManaCost { get; private set; }
    }
}
