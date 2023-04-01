using ExplorationRoguelike.Scripts.Combat;
using System;
using UnityEngine;

public class Player : MonoBehaviour, ICombatant
{
    public int MaxHealth;
    public int Damage;
    public event EventHandler TookTurn;

    [SerializeField]
    private int _currentHealth;
    public int CurrentHealth { get => _currentHealth; }

    public void Awake()
    {
        _currentHealth = MaxHealth;
    }
    public void EnterCombat(ActiveCombat combat)
    {
        combat.Attacked += ReceiveAttack;
    }

    private void ReceiveAttack(object sender, EventArgs e)
    {
        _currentHealth -= 5; // TODO: Add actual damage/effect here from the event.
    }
    public void TakeTurn(EventArgs e)
    {
        TookTurn?.Invoke(this, e);
    }
    public void ExitCombat(ActiveCombat combat)
    {
        throw new NotImplementedException();
    }
}
