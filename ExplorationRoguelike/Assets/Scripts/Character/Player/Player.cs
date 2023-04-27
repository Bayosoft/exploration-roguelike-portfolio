using ExplorationRoguelike;
using System;
using UnityEngine;

public class Player : Character, ICombatant
{
    [SerializeField]
    private HealthComponent _healthComponent;
    public HealthComponent HealthComponent { get => _healthComponent; }

    [SerializeField]
    private AbilityComponent _abilityComponent;
    public AbilityComponent AbilityComponent { get => _abilityComponent; }

    public void Awake()
    {
        //   _currentHealth = MaxHealth;
    }
    public void EnterCombat(CombatStateComponent combat)
    {
        // Combat = combat;
        //   combat.Enemy.TurnTaken += ReceiveAttack;
    }

    /*    public void ReceiveAttack(object sender, TurnTakenEventArgs e)
        {
          //  _currentHealth -= e.Damage;

            //if (_currentHealth == 0) Combat.OnDeath(this);
        }*/
    public void ExecuteTurn(TakeTurnEventArgs e)
    {
        /*        TurnTakenEventArgs turnTakenEventArgs = new(e.Target, e.Attacker, e.Damage);

                TurnTaken?.Invoke(this, turnTakenEventArgs);*/
    }
    public void ExitCombat(CombatStateComponent combat)
    {
        throw new NotImplementedException();
    }
}
