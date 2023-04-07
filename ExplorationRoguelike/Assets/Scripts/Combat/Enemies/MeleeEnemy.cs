using ExplorationRoguelike;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goblin", menuName = "ScriptableObjects/Enemies/Melee", order = 1)]
public class MeleeEnemy : EnemyBase
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void TakeTurn(TakeTurnEventArgs e)
    {
        base.TakeTurn(e);
    }
}
