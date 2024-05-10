using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Characters.PlayerCharacter.Events;
using ExplorationRoguelike.Combat.Events;
using ExplorationRoguelike.GameState;
using ExplorationRoguelike.GUI.Card;
using ExplorationRoguelike.GUI.Character;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        public GameObject enemyPrefab;
        public GameObject cardPrefab;
        public GameObject healthPrefab;
        public TextMeshProUGUI manaText;
        public ObservableCollection<GameObject> HealthViews { get; set; }
        public ObservableCollection<GameObject> CardViews { get; set; }

        // public ObservableCollection<AbilitySO> DrawnCards { get { return new ObservableCollection<AbilitySO>(_combat.CardDeckComponent.CardsDrawn); } }

        public object SelectedCard { get; set; }
        public TextMeshProUGUI enemyIntentText;

        [SerializeField]
        private ScriptableEvent combatEvent;

        public void Awake()
        {
            enemies = new List<CombatNpc>();
            CardViews = new ObservableCollection<GameObject>();
            HealthViews = new ObservableCollection<GameObject>();
        }

        public void Initialize(Player player, CharacterData enemyData)
        {
            this.player = player;

            var enemy = enemyPrefab.GetComponent<CombatNpc>();
            enemy.CharacterData = enemyData;
            var enemyInstance = Instantiate(enemyPrefab);
            enemyInstance.transform.SetParent(this.transform);
            enemies.Add(enemyInstance.GetComponent<CombatNpc>());

            playerTurnComponent = (PlayerTurnComponent)player.TurnComponent;
            enemyTurnComponent = (NpcTurnComponent)enemies[0].TurnComponent;

            playerTurnComponent.CardDeckComponent.CardsDrawn.CollectionChanged += UpdateCards;
            playerTurnComponent.CardDeckComponent.OnManaChanged += UpdateMana;
            enemyTurnComponent.NpcCombatComponent.OnDeclaredIntent += UpdateEnemyIntent;

            playerTurnComponent.CardDeckComponent.InstantiateCards();

            SpawnHealthViews();
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

        private void UpdateMana(object sender, int newMana)
        {
            manaText.text = $"Mana: {newMana}/4";
        }

        private void UpdateEnemyIntent(object sender, IIntentfulAbility intent)
        {
            if (intent == null || intent.IntentTag == null)
            {
                enemyIntentText.text = $"???";
                return;
            }

            enemyIntentText.text = $"{intent.IntentTag.ToString()}";

        }
        private void SpawnHealthViews()
        {
            List<ICombatant> combatants = new(enemies);

            // Player
            var healthView = Instantiate(healthPrefab, this.transform);

            healthView.GetComponent<HealthViewModel>().Initialize(player.HealthComponent);
            healthView.transform.localPosition = new Vector2(-400, 0);
            healthView.transform.localScale = Vector2.one;
            HealthViews.Add(healthView);

            // Enemies
            foreach (var combatant in combatants)
            {
                GameObject eHealthView = Instantiate(healthPrefab, this.transform);
                eHealthView.GetComponent<HealthViewModel>().Initialize(combatant.HealthComponent);
                eHealthView.transform.localPosition = new Vector2(400, 0);
                eHealthView.transform.localScale = Vector2.one;
                HealthViews.Add(eHealthView);
            }
        }

        public void UpdateCards(object sender, NotifyCollectionChangedEventArgs e)
        {
            //different kind of changes that may have occurred in collection
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Card c = (Card)e.NewItems[0];
                GameObject cardView = c.gameObject;
                cardView.transform.SetParent(transform);
                cardView.transform.localPosition = new Vector2(CardViews.Count * 100, 0);
                cardView.transform.localPosition = new Vector2(-300 + (CardViews.Count * 100), -350f);
                cardView.transform.localScale = Vector2.one;
                cardView.transform.SetAsLastSibling();

                cardView.SetActive(true);
                CardViews.Add(cardView);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Card removedCard in e.OldItems)
                {
                    foreach (GameObject cardView in CardViews.ToList())
                    {
                        if (cardView.GetComponent<Card>() == removedCard)
                        {
                            cardView.SetActive(false);
                            CardViews.Remove(cardView);
                            return;
                        }
                    }
                }
            }
        }

        public void OnTryPlayCard(ConcreteEventArgs args)
        {
            var eventArgs = args.ValidateEventArgs<TryPlayCardEventArgs>();
            if (eventArgs.CardView != null && playerTurnComponent.MyTurn)
            {
                playerTurnComponent.CardDeckComponent.PlayCard(eventArgs.CardView, new List<AbilitySystemComponent>() { enemies[0].AbilitySystemComponent });
            }
        }

        public void OnEndTurn()
        {
            if (playerTurnComponent.MyTurn)
            {
                playerTurnComponent.EndTurn();
            }
        }

        public void OnCombatEvent(ConcreteEventArgs eventArgs)
        {
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(this);

        }
        public void OnDeath(ConcreteEventArgs eventArgs)
        {
            OnDeathEventArgs onDeathEventArgs = eventArgs.ValidateEventArgs<OnDeathEventArgs>();
            if (onDeathEventArgs.DeadCharacter is Player)
            {
                _currentState = CombatState.Lost;
            }
            else if (onDeathEventArgs.DeadCharacter is CombatNpc combatNpc)
            {
                EndCombat();
            }
        }

        [SerializeField]
        private LootTransition lootTransition;
        public void EndCombat()
        {
            gameObject.SetActive(false);
            lootTransition.Transition(enemies[0].CharacterData.LootTable, ChangeGamestate);
            TimeHandler.ChangeTimeDurationType(GameplayDurationType.Time);
            TimeHandler.AdvanceTime(2);
        }

        private void ChangeGamestate()
        {
            SceneManager.UnloadSceneAsync("CombatScene");
        }

        public void OnDestroy()
        {
            playerTurnComponent.CardDeckComponent.CardsDrawn.CollectionChanged -= UpdateCards;
            playerTurnComponent.CardDeckComponent.OnManaChanged -= UpdateMana;
            enemyTurnComponent.NpcCombatComponent.OnDeclaredIntent -= UpdateEnemyIntent;
        }
    }
}
