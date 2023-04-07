using ExplorationRoguelike;
using ExplorationRoguelike.Scripts.Combat;
using System;
using UnityEngine;

public class Player : MonoBehaviour, ICombatant
{
    public int MaxHealth;
    public int Damage;
    public event EventHandler<TakeTurnEventArgs> TookTurn;

    [SerializeField]
    private int _currentHealth;
    public int CurrentHealth { get => _currentHealth; }

    public void Awake()
    {
        _currentHealth = MaxHealth;
    }
    public void EnterCombat(ActiveCombat combat)
    {
   //     combat.Attacked += ReceiveAttack;
    }

    public void ReceiveAttack(TakeTurnEventArgs e)
    {
        _currentHealth -= e.Damage; 
    }
    public void TakeTurn(TakeTurnEventArgs e)
    {
        TookTurn?.Invoke(this, e);
    }
    public void ExitCombat(ActiveCombat combat)
    {
        throw new NotImplementedException();
    }
}
