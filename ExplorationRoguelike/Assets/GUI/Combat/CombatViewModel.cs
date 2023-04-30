using UnityEngine;
using ExplorationRoguelike;

namespace ExplortationRoguelike.GUI.Combat
{
    public class CombatViewModel : ViewModel
    {
        [SerializeField]
        private CombatStateComponent _combat;

        public AbilitySO SelectedPlayerAbility { get { return _combat.Player.AbilityComponent.KnownAbilities.Abilities[0]; } }
        public AbilitySO SelectedEnemyAbility { get { return _combat.Enemy.AbilityComponent.KnownAbilities.Abilities[0]; } }

        public int PlayerHealth { get {  return _combat.Player.HealthComponent.CurrentHealth; } }
        public int EnemyHealth { get { return _combat.Enemy.HealthComponent.CurrentHealth; } }
        public string CombatState { get { return _combat.CurrentState.ToString(); } }

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

        private void OnValidate()
        {
        }

        public void OnEnemyTakeTurn(object damage)
        {
           _combat.HandleTurn(_combat.Enemy, _combat.Player, SelectedPlayerAbility);
        }
        public void OnPlayerTakeTurn(object damage)
        {
          //  TakeTurnEventArgs turnEvent = new(_activeCombat.Enemy, _activeCombat.Player, (int)damage);
          //  _activeCombat.HandleTurnEvent(turnEvent);
        }
    }
}


