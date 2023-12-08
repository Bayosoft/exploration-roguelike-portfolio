using Godot;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.FireBuff
{
    [GlobalClass]
    public partial class FireBuffAbility : SelfStatusEffectAbility, IPlayableCard
    {
        [Export]
        public TargetType TargetType { get; private set; }

        [Export]
        public int ManaCost { get; private set; }
    }
}
