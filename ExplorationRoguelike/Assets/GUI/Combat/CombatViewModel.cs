using UnityEngine;
using ExplorationRoguelike;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExplorationRoguelike.GUI.Card;
using ExplorationRoguelike.Assets.GUI;

namespace ExplortationRoguelike.GUI.Combat
{
    public class CombatViewModel : ViewModel
    {
        [SerializeField]
        private CombatStateComponent _combat;

        private ObservableCollection<object> _cardViews;
        public ObservableCollection<object> CardViews { get { return _cardViews; } set { _cardViews = value; OnPropertyChanged("CardViews"); } }
        // public ObservableCollection<AbilitySO> DrawnCards { get { return new ObservableCollection<AbilitySO>(_combat.CardDeckComponent.CardsDrawn); } }

        public object SelectedCard { get; set; }
        public GameplayAbility EnemyIntent { get { return _combat.NpcTurnComponent.DeclaredAbility; } }

        public int PlayerHealth { get { return _combat.Player.HealthComponent.CurrentHealth; } }
        public int EnemyHealth { get { return _combat.Enemy.HealthComponent.CurrentHealth; } }

        private string _combatState;
        public string CombatState
        {
            get
            {
                return _combatState;
            }
            set
            {
                _combatState = value;
                OnPropertyChanged("CombatState");
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
        private DelegateCommand _enemyTakeTurnCommand;
        public DelegateCommand EnemyTakeTurnCommand { get => _enemyTakeTurnCommand; }

        [SerializeField]
        private DelegateCommand _playerTakeTurnCommand;

        public DelegateCommand PlayerTakeTurnCommand { get => _playerTakeTurnCommand; }

        public CombatViewModel()
        {
            _playerTakeTurnCommand = new DelegateCommand(OnPlayerTakeTurn);
            _enemyTakeTurnCommand = new DelegateCommand(OnEnemyTakeTurn);
        }
        void Start()
        {
            GetComponent<NoesisView>().Content.DataContext = this;
            CardViews = new ObservableCollection<object>();
            DrawCards();
        }

        public NoesisXaml CardView;
        public void DrawCards()
        {
            // Draw logic
            foreach (GameplayAbility ability in _combat.CardDeckComponent.CardsInDeck)
            {
                CardViewModel cardViewModel = new(ability);
                //ViewBuilder.CreateCardView(cardViewModel);
                
                object cardView = CardView.Load();          
                
                ((CardView)cardView).DataContext = cardViewModel;

                CardViews.Add(cardView);
            }
        }
        public void OnEnemyTakeTurn(object ability)
        {
            _combat.HandleTurn(_combat.Enemy, new List<AbilitySystemComponent>() { _combat.Player.AbilitySystemComponent }, _combat.NpcTurnComponent.DeclaredAbility);
            CombatState = _combat.CurrentState.ToString();

        }
        public void OnPlayerTakeTurn(object ev)
        {
            if(SelectedCard != null)
            {
                GameplayAbility ability = ((CardViewModel)((CardView)SelectedCard).DataContext).Ability;

                _combat.HandleTurn(_combat.Player, new List<AbilitySystemComponent>() { _combat.Enemy.AbilitySystemComponent }, ability);
            }

            CombatState = _combat.CurrentState.ToString();
            OnPropertyChanged("EnemyHealth");
            OnPropertyChanged("PlayerHealth");
        }

        public void OnCombatEvent(ConcreteEventArgs eventArgs)
        {
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(eventArgs, this);

            Message = combatEventArgs.Message;
        }
    }
}


