using ExplorationRoguelike.Assets.Scripts.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public Dictionary<ICombatant, bool> CombatantTurns;

        public CardDeckComponent CardDeckComponent { get; private set; }
        public NpcTurnComponent NpcTurnComponent { get; private set; } 
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
            CardDeckComponent = new(Player.AbilitySystemComponent.GrantedAbilities);
            NpcTurnComponent = new(Enemy.AbilitySystemComponent.GrantedAbilities);

            CombatantTurns = new() {
                { Player, true },
                { Enemy, false }
            };

            _currentState = CombatState.START;

            // Draw cards, determine pre-start modifiers..

            NpcTurnComponent.DeclareIntent();

            _currentState = CombatState.PLAYERTURN;
        }

        public void HandleTurn(ICombatant instigator, IEnumerable<AbilitySystemComponent> targets, AbilityData ability)
        {
            if (CombatantTurns[instigator] == false)
            {
                _combatEvent.RaiseEvent(new CombatEventArgs("Cannot play card, it is not your turn", false));
                return;
            }

            var activated = instigator.AbilitySystemComponent.TryActivateAbility(ability, targets);

            if (activated)
            {
                CombatantTurns[instigator] = false;
                var nextCombatant = CombatantTurns.Single(kvp => kvp.Key != instigator).Key;

                CombatantTurns[nextCombatant] = true;

                if ((object)nextCombatant == Player)
                {
                    _currentState = CombatState.PLAYERTURN;
                    NpcTurnComponent.DeclareIntent();
                } 
                else if ((object)nextCombatant == Enemy)
                {
                    _currentState = CombatState.ENEMYTURN;
                    HandleTurn(Enemy, new List<AbilitySystemComponent>() { Player.AbilitySystemComponent }, NpcTurnComponent.DeclaredAbility);
                }
            }

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
