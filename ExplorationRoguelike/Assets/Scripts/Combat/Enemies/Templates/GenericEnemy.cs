using ExplorationRoguelike;
using ExplorationRoguelike.Scripts.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

[CreateAssetMenu(fileName = "Generic Enemy", menuName = "ScriptableObjects/Enemies/Generic", order = 1)]
public class GenericEnemy : ScriptableObject
{
    [field: SerializeField]
    public int MaxHealth { get; set; }

    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public string Description { get; set; }
    [field: SerializeField]
    public EnemyTypeEnum EnemyType { get; set; }
}
