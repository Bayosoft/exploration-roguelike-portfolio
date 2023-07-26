using UnityEngine;
using ExplorationRoguelike;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExplorationRoguelike.GUI.Card;
using ExplorationRoguelike.Assets.GUI;
using System;
using System.Collections.Specialized;
using UnityEditor.Playables;
using System.Linq;

namespace ExplortationRoguelike.GUI.Combat
{
    public class CombatViewModel : ViewModel
    {
        private CombatStateComponent _combat;

        private ObservableCollection<object> _cardViews;
        public ObservableCollection<object> CardViews 
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
        private DelegateCommand _tryPlayCardCommand;

        public DelegateCommand TryPlayCardCommand { get => _tryPlayCardCommand; }


        [SerializeField]
        private DelegateCommand _endTurnCommand;

        public DelegateCommand EndTurnCommand { get => _endTurnCommand; }

        public CombatViewModel()
        {
            CardViews = new ObservableCollection<object>();
            _tryPlayCardCommand = new DelegateCommand(OnTryPlayCard);
            _endTurnCommand = new DelegateCommand(OnEndTurnCommand);
        }

        public void SetCombatStateComponent(CombatStateComponent component)
        {
            _combat = component;
        }

        public void Start()
        {
            _combat.PlayerTurnComponent.CardDeckComponent.CardsDrawn.CollectionChanged += new NotifyCollectionChangedEventHandler(UpdateCards);
        }

        public void UpdateCards(object sender, NotifyCollectionChangedEventArgs e)
        {
            //different kind of changes that may have occurred in collection
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                CardViewModel cardViewModel = new((GameplayAbility)e.NewItems[0]);

                CardView cardView = ViewModelComponent.CardXamlView.Load() as CardView;
                cardView.DataContext = cardViewModel;

                CardViews.Add(cardView);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(GameplayAbility removedCard in e.OldItems)
                {
                    foreach(CardView cardView in CardViews.ToList())
                    {
                        if(((CardViewModel)cardView.DataContext).Ability == removedCard)
                        {
                            CardViews.Remove(cardView);
                            return;
                        }
                    }
                }
            }
        }

        public void OnTryPlayCard(object evt)
        {
            if(SelectedCard != null && _combat.PlayerTurnComponent.MyTurn)
            {
                GameplayAbility ability = ((CardViewModel)((CardView)SelectedCard).DataContext).Ability;

                _combat.PlayerTurnComponent.Act(ability, new List<AbilitySystemComponent>() { _combat.Enemies[0].AbilitySystemComponent });
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
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(eventArgs, this);

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


