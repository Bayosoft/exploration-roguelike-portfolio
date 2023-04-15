using System.ComponentModel;
using System;
using ExplortationRoguelike.GUI.Combat;
using UnityEngine;
using ExplorationRoguelike.Scripts.Combat;
using static UnityEditor.Rendering.CameraUI;
using ExplorationRoguelike;

namespace ExplortationRoguelike.GUI.Combat
{
    public class CombatViewModel : ViewModel
    {
        private ActiveCombat _activeCombat;

/*        public int SelectedPlayerCard { get { return _activeCombat.Player.Damage;  } }
      //  public int SelectedEnemyCard { get { return _activeCombat.Enemy.Damage; } }

        public int PlayerHealth { get {  return _activeCombat.Player.CurrentHealth; } }
        public int EnemyHealth { get { return _activeCombat.Enemy.CurrentHealth; } }
        public string CombatState { get { return _activeCombat.CurrentState.ToString(); } }*/

        [SerializeField]
        private DelegateCommand _enemyTurnTakenCommand;
        public DelegateCommand EnemyTurnTakenCommand { get => _enemyTurnTakenCommand; }

        [SerializeField]
        private DelegateCommand _playerTurnTakenCommand;
        public DelegateCommand PlayerTurnTakenCommand { get => _playerTurnTakenCommand; }

        public CombatViewModel()
        {
            _playerTurnTakenCommand = new DelegateCommand(OnPlayerTurnTaken);
            _enemyTurnTakenCommand = new DelegateCommand(OnEnemyTurnTaken);
        }
        void Start()
        {
            GetComponent<NoesisView>().Content.DataContext = this;
        }
        private void Awake()
        {
            _activeCombat = GameObject.Find("CombatManager").GetComponent<ActiveCombat>();
        }
        private void OnValidate()
        {
        }

        public void OnEnemyTurnTaken(object damage)
        {
           // TakeTurnEventArgs turnEvent = new(_activeCombat.Player, _activeCombat.Enemy, (int)damage);
           
          //  _activeCombat.HandleTurnEvent(turnEvent);
            OnPropertyChanged("PlayerHealth");
            OnPropertyChanged("CombatState");
        }
        public void OnPlayerTurnTaken(object damage)
        {
          //  TakeTurnEventArgs turnEvent = new(_activeCombat.Enemy, _activeCombat.Player, (int)damage);
          //  _activeCombat.HandleTurnEvent(turnEvent);
            OnPropertyChanged("EnemyHealth");
            OnPropertyChanged("CombatState");
        }
    }
}


