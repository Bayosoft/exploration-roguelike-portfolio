using ExplorationRoguelike.GameplayTags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Race", menuName = "Character/New Race")]
    public class Race : ScriptableObject
    {
        GameplayTag RaceTag { get; set; }
    }
}