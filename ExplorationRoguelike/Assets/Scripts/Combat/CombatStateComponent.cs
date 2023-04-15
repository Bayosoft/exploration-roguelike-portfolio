using ExplorationRoguelike.Assets.Scripts.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class CombatStateComponent : MonoBehaviour
    {
        public enum CombatState
        {
            START,
            PLAYERTURN,
            ENEMYTURN,
            WON,
            LOST
        };

        private CombatState _currentState;
        public CombatState CurrentState { get { return _currentState; } }

        public event EventHandler EnemyLost;
        public event EventHandler PlayerLost;

        public event EventHandler<TurnTakenEventArgs> OnAttacked;

        public Player Player;
        public Enemy Enemy;

        // Start is called before the first frame update
        void Start()
        {

            DontDestroyOnLoad(gameObject);
            StartCombat();
        }

        public void StartCombat()
        {
            _currentState = CombatState.START;

            Player.TurnTaken += OnTurnTaken;
            Enemy.TurnTaken += OnTurnTaken;

            _currentState = CombatState.PLAYERTURN;
        }

        public void HandleTurnEvent(TakeTurnEventArgs turnEvent)
        {
            if (turnEvent.Attacker is Player && _currentState == CombatState.PLAYERTURN)
            {
                turnEvent.Attacker.ExecuteTurn(turnEvent);
            }
            else if (turnEvent.Attacker is Enemy && _currentState == CombatState.ENEMYTURN)
            {
                turnEvent.Attacker.ExecuteTurn(turnEvent);
            }
            else Debug.Log($"Not {turnEvent.Attacker}'s turn");
        }
        public void OnTurnTaken(object sender, TurnTakenEventArgs e)
        {
            if (CombatState.PLAYERTURN == _currentState)
            {
                _currentState = CombatState.ENEMYTURN;
            }
            else if (_currentState == CombatState.ENEMYTURN)
            {
                _currentState = CombatState.PLAYERTURN;
            }
        }

        public void OnDeath(ICombatant deadCombatant)
        {
            if (deadCombatant is Player)
            {
                _currentState = CombatState.LOST;
            }
            else if (deadCombatant is Enemy)
            {
                _currentState = CombatState.WON;
            }
        }
    }
}
