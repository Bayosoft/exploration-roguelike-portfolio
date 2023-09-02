using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Combat;
using ExplorationRoguelike.Combat.Events;
using ExplorationRoguelike.GUI.Character;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Combat
{
    public class CombatViewModel : MonoBehaviour
    {
        private CombatStateComponent _combat;

        public GameObject cardPrefab;
        public GameObject healthPrefab;
        public TextMeshProUGUI manaText;
        public ObservableCollection<GameObject> HealthViews { get; set; }
        public ObservableCollection<GameObject> CardViews { get; set; }

        // public ObservableCollection<AbilitySO> DrawnCards { get { return new ObservableCollection<AbilitySO>(_combat.CardDeckComponent.CardsDrawn); } }

        public object SelectedCard { get; set; }
        public TextMeshProUGUI enemyIntentText;

        public void Awake()
        {
            CardViews = new ObservableCollection<GameObject>();
            HealthViews = new ObservableCollection<GameObject>();
            _combat = GameObject.Find("CombatManager").GetComponent<CombatStateComponent>();
            _combat.playerTurnComponent.CardDeckComponent.CardsDrawn.CollectionChanged += UpdateCards;
            _combat.playerTurnComponent.CardDeckComponent.OnManaChanged += UpdateMana;
            _combat.enemyTurnComponent.NpcCombatComponent.OnDeclaredIntent += UpdateEnemyIntent;
        }

        public void Start()
        {
            SpawnHealthViews();
        }

        private void UpdateMana(object sender, int newMana)
        {
            manaText.text = $"Mana: {newMana}/4";
        }

        private void UpdateEnemyIntent(object sender, GameplayAbility intent)
        {
            enemyIntentText.text = $"Enemy Intent: {intent.tags.First().ToString()}";
        }
        private void SpawnHealthViews()
        {
            List<ICombatant> combatants = new(_combat.enemies);

            // Player
            GameObject healthView = Instantiate(healthPrefab, this.transform);

            healthView.GetComponent<HealthViewModel>().Initialize(_combat.player.HealthComponent);
            healthView.transform.localPosition = new Vector2(-400, 0);
            healthView.transform.localScale = Vector2.one;
            HealthViews.Add(healthView);

            // Enemies
            foreach (ICombatant combatant in combatants)
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
                Card.Card c = (Card.Card)e.NewItems[0];
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
                foreach (Card.Card removedCard in e.OldItems)
                {
                    foreach (GameObject cardView in CardViews.ToList())
                    {
                        if (cardView.GetComponent<Card.Card>() == removedCard)
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
            if (eventArgs.Card != null && _combat.playerTurnComponent.MyTurn)
            {
                _combat.playerTurnComponent.CardDeckComponent.PlayCard(eventArgs.Card, new List<AbilitySystemComponent>() { _combat.enemies[0].AbilitySystemComponent });
            }
        }

        public void OnEndTurn()
        {
            if (_combat.playerTurnComponent.MyTurn)
            {
                _combat.playerTurnComponent.EndTurn();
            }
        }

        public void OnCombatEvent(ConcreteEventArgs eventArgs)
        {
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(this);

        }
    }
}


