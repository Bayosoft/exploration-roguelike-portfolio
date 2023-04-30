using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        public Player Player;
        public Enemy Enemy;

        public SortedDictionary<ICombatant, bool> CombatantTurns;

        public CardDeckComponent CardDeckComponent { get; private set; }

        [SerializeField]
        private ScriptableEvent _combatEvent;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            StartCombat();
        }

        public void StartCombat()
        {
            CardDeckComponent = new(Player.AbilityComponent.KnownAbilities.Abilities);

            CombatantTurns = new() {
                { Player, true },
                { Enemy, false }
            };

            _currentState = CombatState.START;

            // Draw cards, determine pre-start modifiers..

            _currentState = CombatState.PLAYERTURN;
        }

        public void HandleTurn(ConcreteEventArgs args)
        {
            TakeTurnEventArgs takeTurnEventArgs = args.ValidateEventArgs<TakeTurnEventArgs>(args, this);

            if (CombatantTurns[takeTurnEventArgs.Attacker] == false)
            {
                _combatEvent.RaiseEvent(new CombatEventArgs("Cannot play card, it is not your turn", false));
                return;
            }

            takeTurnEventArgs.Attacker.AbilityComponent.CanActivateAbility(takeTurnEventArgs.Target, takeTurnEventArgs.Ability);

            /*            if (turnEvent.Attacker is Player && _currentState == CombatState.PLAYERTURN)
                        {
                        //    turnEvent.Attacker.ExecuteTurn(turnEvent);
                        }
                        else if (turnEvent.Attacker is Enemy && _currentState == CombatState.ENEMYTURN)
                        {
                         //   turnEvent.Attacker.ExecuteTurn(turnEvent);
                        }
                        else Debug.Log($"Not {turnEvent.Attacker}'s turn");*/
        }
        /*        public void OnTurnTaken(object sender, TurnTakenEventArgs e)
                {
                    if (CombatState.PLAYERTURN == _currentState)
                    {
                        _currentState = CombatState.ENEMYTURN;
                    }
                    else if (_currentState == CombatState.ENEMYTURN)
                    {
                        _currentState = CombatState.PLAYERTURN;
                    }
                }*/

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
