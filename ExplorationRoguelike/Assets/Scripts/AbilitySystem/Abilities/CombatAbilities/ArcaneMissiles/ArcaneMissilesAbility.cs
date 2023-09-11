using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.ArcaneMissiles
{
    public class ArcaneMissilesAbility : InstantDamageAbility, IPlayableCard
    {
        [field: SerializeField]
        public TargetType TargetType { get; private set; }
        
        [field: SerializeField]
        public int ManaCost { get; private set; }

        [SerializeField] 
        private int missileCount;
        
        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            for (var missilesFired = 0; missilesFired < missileCount; missilesFired++)
            {
                base.Activate(instigator, targets);
            }
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            base.Activate(instigator, target);
        }
    }
}
