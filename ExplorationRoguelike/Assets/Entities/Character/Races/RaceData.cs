using ExplorationRoguelike.GameplayTags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "RaceData", menuName = "RaceData")]
    public class RaceData : ScriptableObject
    {
        GameplayTag RaceTag { get; set; }
    }
}