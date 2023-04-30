using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class Ability : MonoBehaviour
    {
        public AbilitySO AbilityData;
        public ICombatant Source;
        public ICombatant Target;

        public Ability()
        {
            
        }

        public virtual void Activate(AbilityComponent instigator, ActivationType activationType)
        {
            // Overridden by specific abilities
        }
    }
}
