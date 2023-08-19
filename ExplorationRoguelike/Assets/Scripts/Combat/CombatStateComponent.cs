using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat.Events;
using ExplorationRoguelike.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Combat
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
        public CombatState CurrentState { get { return _currentState; } private set { _currentState = value; } }

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
            CurrentState = CombatState.START;
            EnemyTurnComponent.NpcCombatComponent.DeclareIntent();
            PlayerTurnComponent.StartTurn();
        }

        public void OnTurnEnded(ConcreteEventArgs eventArgs)
        {
            var endTurnEventArgs = eventArgs.ValidateEventArgs<EndTurnEventArgs>(eventArgs);

            if (endTurnEventArgs.Initiator is PlayerTurnComponent)
            {
                EnemyTurnComponent.StartTurn();
                EnemyTurnComponent.Act(EnemyTurnComponent.NpcCombatComponent.DeclaredAbility, new List<AbilitySystemComponent>() { Player.AbilitySystemComponent });

                CurrentState = CombatState.ENEMYTURN;
            }
            else if (endTurnEventArgs.Initiator is NpcTurnComponent)
            {
                PlayerTurnComponent.StartTurn();
                CurrentState = CombatState.PLAYERTURN;
            }
        }

        public void OnDeath<T>(T deadCombatant)
        {
            if (deadCombatant is Player)
            {
                _currentState = CombatState.LOST;
            }
        }
    }
}
