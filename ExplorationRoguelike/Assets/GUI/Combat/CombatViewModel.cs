using UnityEngine;
using ExplorationRoguelike;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExplorationRoguelike.GUI.Card;
using ExplorationRoguelike.Assets.GUI;
using System;

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
            DrawCards();
        }

        public void DrawCards()
        {
            // Draw logic
            foreach (GameplayAbility ability in _combat.PlayerTurnComponent.CardDeckComponent.CardsInDeck)
            {
                CardViewModel cardViewModel = new(ability);
                //ViewBuilder.CreateCardView(cardViewModel);
                
                CardView cardView = ViewModelComponent.CardXamlView.Load() as CardView;          
                
                cardView.DataContext = cardViewModel;

                CardViews.Add(cardView);
            }
        }

        public void OnTryPlayCard(object evt)
        {
            if(SelectedCard != null && _combat.PlayerTurnComponent.MyTurn)
            {
                GameplayAbility ability = ((CardViewModel)((CardView)SelectedCard).DataContext).Ability;

                _combat.PlayerTurnComponent.Act(ability, new List<AbilitySystemComponent>() { _combat.Enemies[0].AbilitySystemComponent });
            }

            OnPropertyChanged("EnemyHealth");
            OnPropertyChanged("PlayerHealth");
        }

        public void OnEndTurnCommand(object evt)
        {
            if (_combat.PlayerTurnComponent.MyTurn)
            {
                _combat.PlayerTurnComponent.EndTurn();

                OnPropertyChanged("EnemyIntent");
                OnPropertyChanged("CombatState");
                OnPropertyChanged("PlayerHealth");
            }
        }

        public void OnCombatEvent(ConcreteEventArgs eventArgs)
        {
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(eventArgs, this);

            Message = combatEventArgs.Message;
        }
    }
}


