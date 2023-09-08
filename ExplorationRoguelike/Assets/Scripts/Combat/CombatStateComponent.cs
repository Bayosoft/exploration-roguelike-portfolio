using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Combat
{
    public class CombatStateComponent : MonoBehaviour
    {
        public enum CombatState
        {
            Start,
            Playerturn,
            Enemyturn,
            Won,
            Lost
        };

        private CombatState _currentState;
        public CombatState CurrentState 
        { 
            get => _currentState;
            private set => _currentState = value;
        }
        
        //  public List<Enemy> Allies; Probably not implementing this.
        public List<CombatNpc> enemies;
        public Player player;
        public NpcTurnComponent enemyTurnComponent;
        public PlayerTurnComponent playerTurnComponent;
        
        [SerializeField]
        private ScriptableEvent combatEvent;


        public void Awake()
        {
            enemies = new List<CombatNpc>();
        }

        public void Initialize(GameObject playerPrefab, GameObject enemyPrefab)
        {
            var playerInstance = Instantiate(playerPrefab);
            var enemyInstance = Instantiate(enemyPrefab);

            player = playerInstance.GetComponent<Player>();
            enemies.Add(enemyInstance.GetComponent<CombatNpc>());
            
            playerTurnComponent = (PlayerTurnComponent)player.TurnComponent;
            enemyTurnComponent = (NpcTurnComponent)enemies[0].TurnComponent;
        }
        // Start is called before the first frame update
        void Start()
        {
            StartCombat();
        }

        public void StartCombat()
        {
            CurrentState = CombatState.Start;
            enemyTurnComponent.NpcCombatComponent.DeclareIntent();
            playerTurnComponent.StartTurn();
        }

        public void OnTurnEnded(ConcreteEventArgs eventArgs)
        {
            var endTurnEventArgs = eventArgs.ValidateEventArgs<EndTurnEventArgs>(eventArgs);

            if (endTurnEventArgs.Initiator is PlayerTurnComponent)
            {
                enemyTurnComponent.StartTurn();
                enemyTurnComponent.Act(enemyTurnComponent.NpcCombatComponent.DeclaredAbility, new List<AbilitySystemComponent>() { player.AbilitySystemComponent });

                CurrentState = CombatState.Enemyturn;
            }
            else if (endTurnEventArgs.Initiator is NpcTurnComponent)
            {
                playerTurnComponent.StartTurn();
                CurrentState = CombatState.Playerturn;
            }
        }

        public void OnDeath<T>(T deadCombatant)
        {
            if (deadCombatant is Player)
            {
                _currentState = CombatState.Lost;
            }
        }
    }
}
