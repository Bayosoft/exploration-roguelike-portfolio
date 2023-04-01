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

        public event EventHandler Attacked;

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

        public void StartTurn() 
        { 
            if(_currentState == CombatState.PLAYERTURN)
            {
                Player.TakeTurn(new EventArgs());

                _currentState = CombatState.ENEMYTURN;
            }
            else if(_currentState == CombatState.ENEMYTURN) 
            { 
                Enemy.TakeTurn(new EventArgs());

                _currentState = CombatState.PLAYERTURN;
            }
        }
        public void OnAttacked(object sender, EventArgs e)
        {

            Attacked?.Invoke(this, e);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}