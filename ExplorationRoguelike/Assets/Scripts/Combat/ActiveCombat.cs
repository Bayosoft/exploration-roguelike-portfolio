using System;
using UnityEngine;

namespace ExplorationRoguelike.Scripts.Combat
{
    public class ActiveCombat : MonoBehaviour
    {
        private enum CombatState
        {
            START,
            PLAYERTURN,
            ENEMYTURN,
            WON,
            LOST
        };

        private CombatState _currentState;

        public event EventHandler EnemyLost;
        public event EventHandler PlayerLost;

        public event EventHandler<TakeTurnEventArgs> Attacked;

        public Player Player;
        public EnemyBase Enemy;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            StartCombat();
        }

        public void StartCombat()
        {
            _currentState = CombatState.START;

            Enemy.TookTurn += OnAttacked;
            Player.TookTurn += OnAttacked;

            Player.EnterCombat(this);
            Enemy.EnterCombat(this);

            _currentState = CombatState.PLAYERTURN; 
        }

        public void HandleTurnEvent(TakeTurnEventArgs turnEvent) 
        {
            turnEvent.Attacker.TakeTurn(turnEvent);

           // else Debug.Log($"Not {turnEvent.Attacker}'s turn");
        }
        public void OnAttacked(object sender, TakeTurnEventArgs e)
        {
            e.Target.ReceiveAttack(e);
//            Attacked?.Invoke(this, e);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}