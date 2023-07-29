using UnityEngine;
using ExplorationRoguelike;
using System.Collections.ObjectModel;
using ExplorationRoguelike.GUI.Card;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;

namespace ExplortationRoguelike.GUI.Combat
{
    public class CombatViewModel : ViewModel
    {
        private CombatStateComponent _combat;

        public GameObject CardPrefab;

        private ObservableCollection<GameObject> _cardViews;
        public ObservableCollection<GameObject> CardViews 
        { 
            get { return _cardViews; } 
            set { _cardViews = value; OnPropertyChanged("CardViews"); } 
        }

        // public ObservableCollection<AbilitySO> DrawnCards { get { return new ObservableCollection<AbilitySO>(_combat.CardDeckComponent.CardsDrawn); } }

        public object SelectedCard { get; set; }
        public GameplayAbility EnemyIntent { get { return _combat.EnemyTurnComponent.NpcCombatComponent.DeclaredAbility; } } 

        public int PlayerHealth 
        { 
            get { return Mathf.CeilToInt(_combat.Player.HealthComponent.CurrentHealth); } 
        }

        public int PlayerMana
        {
            get { return _combat.Player.CardDeckComponent.Mana; }
        }


        public int EnemyHealth 
        { 
            get { return Mathf.CeilToInt(_combat.Enemies[0].HealthComponent.CurrentHealth); }
        }

        public string EnemyName 
        { 
            get { return _combat.Enemies[0].CharacterData.Name.Length > 0 ? _combat.Enemies[0].CharacterData.Name : "Enemy"; }
        }
        public string CombatState
        {
            get
            {
                return _combat.CurrentState.ToString();
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }
        [SerializeField]
        private DelegateCommand _endTurnCommand;

        public DelegateCommand EndTurnCommand { get => _endTurnCommand; }

        public void Awake()
        {
            CardViews = new ObservableCollection<GameObject>();
            _endTurnCommand = new DelegateCommand(OnEndTurnCommand);
            _combat = GameObject.Find("CombatManager").GetComponent<CombatStateComponent>();
            _combat.PlayerTurnComponent.CardDeckComponent.CardsDrawn.CollectionChanged += new NotifyCollectionChangedEventHandler(UpdateCards);
        }

        public void UpdateCards(object sender, NotifyCollectionChangedEventArgs e)
        {
            //different kind of changes that may have occurred in collection
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                CardPrefab.GetComponent<CardViewModel>().Initialize((CardAbility)e.NewItems[0]);
                // Create card prefab in world
                GameObject cardView = Instantiate(CardPrefab);
                cardView.transform.parent = gameObject.transform;
                cardView.transform.localPosition = new Vector2(CardViews.Count * 100, 0);
                cardView.transform.localScale = Vector2.one;
                CardViews.Add(cardView);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(GameplayAbility removedCard in e.OldItems)
                {
                    foreach(GameObject cardView in CardViews.ToList())
                    {
                        if(cardView.GetComponent<CardViewModel>().Ability == removedCard)
                        {
                            Destroy(cardView);
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
            if(eventArgs.Card != null && _combat.PlayerTurnComponent.MyTurn)
            {
                _combat.PlayerTurnComponent.Act(eventArgs.Card, new List<AbilitySystemComponent>() { _combat.Enemies[0].AbilitySystemComponent });
            }

            UpdateUI();
        }

        public void OnEndTurnCommand(object evt)
        {
            if (_combat.PlayerTurnComponent.MyTurn)
            {
                _combat.PlayerTurnComponent.EndTurn();

                UpdateUI();
            }
        }

        public void OnCombatEvent(ConcreteEventArgs eventArgs)
        {
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(this);

            Message = combatEventArgs.Message;
        }

        public void UpdateUI()
        {
            OnPropertyChanged("EnemyIntent");
            OnPropertyChanged("CombatState");
            OnPropertyChanged("PlayerHealth");
            OnPropertyChanged("PlayerMana");
            OnPropertyChanged("EnemyHealth");
        }
    }
}


