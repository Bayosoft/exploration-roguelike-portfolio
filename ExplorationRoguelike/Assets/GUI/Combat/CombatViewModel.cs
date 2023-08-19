using UnityEngine;
using System.Collections.ObjectModel;
using ExplorationRoguelike.GUI.Card;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using ExplorationRoguelike.GUI.Character;
using ExplorationRoguelike.Combat;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Scripts;
using ExplorationRoguelike.Combat.Events;
using ExplorationRoguelike.AbilitySystem;

namespace ExplortationRoguelike.GUI.Combat
{
    public class CombatViewModel : MonoBehaviour
    {
        private CombatStateComponent _combat;

        public GameObject CardPrefab;
        public GameObject HealthPrefab;
        public TextMeshProUGUI ManaText;
        public ObservableCollection<GameObject> HealthViews { get; set; }
        public ObservableCollection<GameObject> CardViews { get; set; }

        // public ObservableCollection<AbilitySO> DrawnCards { get { return new ObservableCollection<AbilitySO>(_combat.CardDeckComponent.CardsDrawn); } }

        public object SelectedCard { get; set; }
        public TextMeshProUGUI EnemyIntentText;

        public void Awake()
        {
            CardViews = new ObservableCollection<GameObject>();
            HealthViews = new ObservableCollection<GameObject>();
            _combat = GameObject.Find("CombatManager").GetComponent<CombatStateComponent>();
            _combat.PlayerTurnComponent.CardDeckComponent.CardsDrawn.CollectionChanged += new NotifyCollectionChangedEventHandler(UpdateCards);
            _combat.PlayerTurnComponent.CardDeckComponent.OnManaChanged += UpdateMana;
            _combat.EnemyTurnComponent.NpcCombatComponent.OnDeclaredIntent += UpdateEnemyIntent;
        }

        public void Start()
        {
            SpawnHealthViews();
        }

        private void UpdateMana(object sender, int newMana)
        {
            ManaText.text = $"Mana: {newMana}/4";
        }

        private void UpdateEnemyIntent(object sender, GameplayAbility intent)
        {
            EnemyIntentText.text = $"Enemy Intent: {intent.Tags.First().ToString()}";
        }
        private void SpawnHealthViews()
        {
            List<ICombatant> combatants = new(_combat.Enemies);

            // Player
            GameObject healthView = Instantiate(HealthPrefab, this.transform);

            healthView.GetComponent<HealthViewModel>().Initialize(_combat.Player.HealthComponent);
            healthView.transform.localPosition = new Vector2(-400, 0);
            healthView.transform.localScale = Vector2.one;
            HealthViews.Add(healthView);

            // Enemies
            foreach (ICombatant combatant in combatants)
            {
                GameObject eHealthView = Instantiate(HealthPrefab, this.transform);
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
                cardView.transform.parent = gameObject.transform;
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
            if (eventArgs.Card != null && _combat.PlayerTurnComponent.MyTurn)
            {
                _combat.PlayerTurnComponent.CardDeckComponent.PlayCard(eventArgs.Card, new List<AbilitySystemComponent>() { _combat.Enemies[0].AbilitySystemComponent });
            }
        }

        public void OnEndTurn()
        {
            if (_combat.PlayerTurnComponent.MyTurn)
            {
                _combat.PlayerTurnComponent.EndTurn();
            }
        }

        public void OnCombatEvent(ConcreteEventArgs eventArgs)
        {
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(this);

        }
    }
}


