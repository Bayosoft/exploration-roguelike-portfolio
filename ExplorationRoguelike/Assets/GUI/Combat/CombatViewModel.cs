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
        private CombatStateComponent _combat;

        private ObservableCollection<object> _cardViews;
        public ObservableCollection<object> CardViews 
        { 
            get { return _cardViews; } 
            set { _cardViews = value; OnPropertyChanged("CardViews"); } 
        }

        // public ObservableCollection<AbilitySO> DrawnCards { get { return new ObservableCollection<AbilitySO>(_combat.CardDeckComponent.CardsDrawn); } }

        public object SelectedCard { get; set; }
        public GameplayAbility EnemyIntent { get { return _combat.EnemyTurnComponent.CombatPlayComponent.DeclaredAbility; } } 

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
        private DelegateCommand _tryPlayCardCommand;

        public DelegateCommand TryPlayCardCommand { get => _tryPlayCardCommand; }

        public CombatViewModel()
        {
            CardViews = new ObservableCollection<object>();
            _tryPlayCardCommand = new DelegateCommand(OnTryPlayCard);
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
            foreach (GameplayAbility ability in _combat.PlayerTurnComponent.CombatPlayComponent.CardsInDeck)
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

                _combat.PlayerTurnComponent.CombatPlayComponent.PlayCard(ability, new List<AbilitySystemComponent>() { _combat.Enemies[0].AbilitySystemComponent });
            }

            CombatState = _combat.CurrentState.ToString();
            OnPropertyChanged("EnemyHealth");
            OnPropertyChanged("PlayerHealth");
            OnPropertyChanged("EnemyIntent");
        }

        public void OnCombatEvent(ConcreteEventArgs eventArgs)
        {
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(eventArgs, this);

            Message = combatEventArgs.Message;
        }
    }
}


