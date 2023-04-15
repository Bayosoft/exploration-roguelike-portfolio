using ExplorationRoguelike;
using ExplorationRoguelike.Assets.Scripts.Combat;
using ExplorationRoguelike.Scripts.Combat;
using System;
using UnityEngine;

public class Player : Character, ICombatant
{
    public HealthComponent HealthComponent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int Damage;
    public event EventHandler<TurnTakenEventArgs> TurnTaken;

    [SerializeField]
    private int _currentHealth;
    public int CurrentHealth { get => _currentHealth; }
    public ActiveCombat Combat { get; private set; }

    public void Awake()
    {
     //   _currentHealth = MaxHealth;
    }
    public void EnterCombat(ActiveCombat combat)
    {
        Combat = combat;
     //   combat.Enemy.TurnTaken += ReceiveAttack;
    }

    public void ReceiveAttack(object sender, TurnTakenEventArgs e)
    {
        _currentHealth -= e.Damage;

        //if (_currentHealth == 0) Combat.OnDeath(this);
    }
    public void ExecuteTurn(TakeTurnEventArgs e)
    {
        TurnTakenEventArgs turnTakenEventArgs = new(e.Target, e.Attacker, e.Damage);

        TurnTaken?.Invoke(this, turnTakenEventArgs);
    }
    public void ExitCombat(ActiveCombat combat)
    {
        throw new NotImplementedException();
    }
}
