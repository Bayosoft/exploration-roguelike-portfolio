using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.Overload
{
    public class OverloadAbility : SelfStatusEffectAbility, IPlayableCard
    {
        [field: SerializeField]
        public TargetType TargetType { get; private set; }

        [field: SerializeField]
        public int ManaCost { get; private set; }
    }
}
