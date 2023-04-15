using ExplorationRoguelike.Assets.Scripts.Combat;
using ExplorationRoguelike.Scripts.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class Enemy : Character, ICombatant
    {
        public AbilityComponent AbilityComponent;

/*        public int Damage { get => EnemyBase.Damage; }
        public int MaxHealth { get => EnemyBase.MaxHealth; }*/

        private int _currentHealth;
        public int CurrentHealth 
        { 
            get { return _currentHealth; } 
            private set 
            { 
                _currentHealth = value; 
                if (_currentHealth == 0) 
                { 
                   // CombatantDied?.Invoke(); 
                } 
            }
        }

        public ActiveCombat Combat { get; private set; }
        public HealthComponent HealthComponent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<TurnTakenEventArgs> TurnTaken;

        void Start()
        {
         //   CurrentHealth = MaxHealth;
        }

        public virtual void EnterCombat(ActiveCombat combat)
        {
            Combat = combat;
          //  combat.Player.TurnTaken += ReceiveAttack;
        }

        public void ReceiveAttack(object sender, TurnTakenEventArgs e)
        {
            CurrentHealth -= e.Damage;

        //    if (CurrentHealth == 0) Combat.OnDeath(this);
        }

        public void ExitCombat(ActiveCombat combat)
        {
            throw new NotImplementedException();
        }

        public virtual void ExecuteTurn(TakeTurnEventArgs e)
        {
            TurnTakenEventArgs turnTakenEventArgs = new(e.Target, e.Attacker, e.Damage);
            TurnTaken?.Invoke(this, turnTakenEventArgs);
        }
    }
}
