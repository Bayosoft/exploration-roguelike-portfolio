using ExplorationRoguelike;
using ExplorationRoguelike.Scripts.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public abstract class EnemyBase : ScriptableObject, ICombatant
{
    public int MaxHealth;
    public int Damage;
    public string Description;

    private int _currentHealth;
    public int CurrentHealth { get => _currentHealth; }

    // TODO: (EventHandler<EnemyTakeTurnEventArgs>)
    public event EventHandler<TakeTurnEventArgs> TookTurn;

    public virtual void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public virtual void EnterCombat(ActiveCombat combat)
    {
      //  combat.Attacked += ReceiveAttack;
    }

    public void ReceiveAttack(TakeTurnEventArgs e)
    {
        _currentHealth -= e.Damage;
    }

    public void ExitCombat(ActiveCombat combat)
    {
        throw new NotImplementedException();
    }

    public virtual void TakeTurn(TakeTurnEventArgs e)
    {
        TookTurn?.Invoke(this, e);
    }
}
