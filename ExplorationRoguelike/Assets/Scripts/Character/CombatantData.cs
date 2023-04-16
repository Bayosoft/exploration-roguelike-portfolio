using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Combatant Data", menuName = "ScriptableObjects/Combatant Data", order = 1)]
    public class CombatantData : ScriptableObject
    {
        [field: SerializeField]
        public int Damage { get; set; }
    }
}
