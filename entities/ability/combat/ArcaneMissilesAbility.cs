using ExplorationRoguelike.Characters;
using Godot;
using System.Collections.Generic;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.ArcaneMissiles
{
    public partial class ArcaneMissilesAbility : InstantDamageAbility, IPlayableCard
    {
        [Export]
        public TargetType TargetType { get; private set; }

        [Export]
        public int ManaCost { get; private set; }

        [Export] 
        private int missileCount;
        
        public override void Activate(AbilitySystemComponent instigator, IEnumerable<Character> targets)
        {
            for (var missilesFired = 0; missilesFired < missileCount; missilesFired++)
            {
                base.Activate(instigator, targets);
            }
        }

        public override void Activate(AbilitySystemComponent instigator, Character target)
        {
            base.Activate(instigator, target);
        }
    }
}
