using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{

    [CreateAssetMenu(fileName = "Known Abilities", menuName = "ScriptableObjects/Abilities/Known Abilities", order = 1)]
    public class KnownAbilitiesData : ScriptableObject
    {
        public List<AbilityData> Abilities;
    }
}
