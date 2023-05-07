using UnityEngine;
using ExplorationRoguelike;
using System.Collections.Generic;
using static ExplorationRoguelike.CombatStateComponent;

namespace ExplortationRoguelike.GUI.Combat
{
    public class CombatViewModel : ViewModel
    {
        [SerializeField]
        private CombatStateComponent _combat;

        public AbilitySO SelectedPlayerAbility { get { return _combat.Player.AbilityComponent.KnownAbilities.Abilities[0]; } }
        public AbilitySO SelectedEnemyAbility { get { return _combat.Enemy.AbilityComponent.KnownAbilities.Abilities[0]; } }

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
        }

        public void OnEnemyTakeTurn(object ability)
        {
            _combat.HandleTurn(_combat.Enemy, new List<Character>() { _combat.Player }, new Ability(ability as AbilitySO));
            CombatState = _combat.CurrentState.ToString();
            OnPropertyChanged("PlayerHealth");
        }
        public void OnPlayerTakeTurn(object ability)
        {
            _combat.HandleTurn(_combat.Player, new List<Character>() { _combat.Enemy }, new Ability(ability as AbilitySO));
            CombatState = _combat.CurrentState.ToString();
            OnPropertyChanged("EnemyHealth");
        }

        public void OnCombatEvent(ConcreteEventArgs eventArgs)
        {
            var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(eventArgs, this);

            Message = combatEventArgs.Message;
        }
    }
}


