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

    [SerializeField]
    private int _currentHealth;
    public int CurrentHealth { get => _currentHealth; }

    // TODO: (EventHandler<EnemyTakeTurnEventArgs>)
    public event EventHandler TookTurn;

    public void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public virtual void EnterCombat(ActiveCombat combat)
    {
        combat.Attacked += ReceiveAttack;
    }

    private void ReceiveAttack(object sender, EventArgs e)
    {
        _currentHealth -= 3;
    }

    public void ExitCombat(ActiveCombat combat)
    {
        throw new NotImplementedException();
    }

    public virtual void TakeTurn(EventArgs e)
    {
        TookTurn?.Invoke(this, e);
    }
}
