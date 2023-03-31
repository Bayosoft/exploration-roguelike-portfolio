using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goblin", menuName = "ScriptableObjects/Enemies/Melee", order = 1)]
public class MeleeEnemy : EnemyBase
{
    public override void TakeTurn(EventArgs e)
    {
        //TODO: create EventArgs object for taking turns and make it do damage.
        base.TakeTurn(e);
    }
}
