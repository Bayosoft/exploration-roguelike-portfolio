using ExplorationRoguelike;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "Goblin", menuName = "ScriptableObjects/Enemies/Goblin", order = 1)]
public class Goblin : CharacterData
{
    // Unique racial properties that isnt shared among other races.


    public int RegenerationBonus = 2;

}
