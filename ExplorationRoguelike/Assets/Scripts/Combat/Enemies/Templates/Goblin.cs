using ExplorationRoguelike;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goblin", menuName = "ScriptableObjects/Enemies/Goblin", order = 1)]
public class Goblin : GenericEnemy
{
    [SerializeField]
    public int RegenerationBonus = 2;
}
