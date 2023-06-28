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
        public GameplayAbility EnemyIntent { get { return _combat.NpcTurnComponent.DeclaredAbility; } }

        public int PlayerHealth 
        { 
            get { return Mathf.CeilToInt(_combat.Player.HealthComponent.CurrentHealth); } 
        }

        public int EnemyHealth 
        { 
            get { return Mathf.CeilToInt(_combat.Enemy.HealthComponent.CurrentHealth); }
        }

        public string EnemyName 
        { 
            get { return _combat.Enemy.CharacterData.Name.Length > 0 ? _combat.Enemy.CharacterData.Name : "Enemy"; }
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

        public DelegateCommand TryPlayTurnCommand { get => _tryPlayCardCommand; }

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
            foreach (GameplayAbility ability in _combat.CardDeckComponent.CardsInDeck)
            {
                CardViewModel cardViewModel = new(ability);
                //ViewBuilder.CreateCardView(cardViewModel);
                
                CardView cardView = ViewModelComponent.CardXamlView.Load() as CardView;          
                
                cardView.DataContext = cardViewModel;

                CardViews.Add(cardView);
            }
        }

        public void OnTryPlayCard(object _)
        {
            if(SelectedCard != null && _combat.CombatantTurns[_combat.Player])
            {
                GameplayAbility ability = ((CardViewModel)((CardView)SelectedCard).DataContext).Ability;

                _combat.CardDeckComponent.PlayCard(ability, new List<AbilitySystemComponent>() { _combat.Enemy.AbilitySystemComponent });
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


