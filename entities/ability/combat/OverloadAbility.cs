using Godot;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.Overload
{
    public partial class OverloadAbility : SelfStatusEffectAbility, IPlayableCard
    {
        [Export]
        public TargetType TargetType { get; private set; }

        [Export]
        public int ManaCost { get; private set; }
    }
}
