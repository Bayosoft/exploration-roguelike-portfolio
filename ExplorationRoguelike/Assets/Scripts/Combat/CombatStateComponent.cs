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

      //  public List<Enemy> Allies; Probably not implementing this.
        public List<CombatNpc> Enemies;
        public Player Player;
        public NpcTurnComponent EnemyTurnComponent;
        public PlayerTurnComponent PlayerTurnComponent;


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
            _currentState = CombatState.START;
        }

        public void OnPlayerTurnFinished()
        {
            // Execute enemy turn.
/*            if (CombatantTurns[instigator] == false)
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
            }*/
        }

        public void OnDeath<T>(T deadCombatant)
        {
            if (deadCombatant is INpcCombatant)
            {
                _currentState = CombatState.LOST;
            }
            else if (deadCombatant is IPlayerCombatant)
            {
                _currentState = CombatState.WON;
            }
        }
    }
}
