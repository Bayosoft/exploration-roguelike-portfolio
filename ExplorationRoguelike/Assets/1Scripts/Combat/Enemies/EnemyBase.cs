using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public abstract class EnemyBase : ScriptableObject
{
    public int MaxHealth;
    public int Damage;
    public string Description;

    private int _currentHealth;
    public int CurrentHealth { get => _currentHealth; }

    // TODO: (EventHandler<EnemyTakeTurnEventArgs>)
    public event EventHandler TookTurn;
    
    protected virtual void TakeTurn(EventArgs e)
    {
        TookTurn?.Invoke(this, e);
    }
}
