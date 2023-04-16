using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/Ability", order = 1)]
    public class AbilityData : ScriptableObject
    {
        public string Name;
        public string Description;

        // TODO: Move to DamageAbilityData : AbilityData
        public int minDamage, maxDamage;
    }
}
