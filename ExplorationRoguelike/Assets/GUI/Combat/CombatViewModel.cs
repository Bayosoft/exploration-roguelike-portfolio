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

        public int SelectedPlayerDamage { get; } = 3;
        public int SelectedEnemyDamage { get; } = 5;
        public int PlayerHealth { get {  return _activeCombat.Player.CurrentHealth; } }
        public int EnemyHealth { get { return _activeCombat.Enemy.CurrentHealth; } }

        [SerializeField]
        private DelegateCommand _enemyAttackedCommand;
        public DelegateCommand EnemyAttackedCommand { get => _enemyAttackedCommand; }

        [SerializeField]
        private DelegateCommand _playerAttackedCommand;
        public DelegateCommand PlayerAttackedCommand { get => _playerAttackedCommand; }

        /*        private Quest _selectedQuest;
                public Quest SelectedQuest
                {
                    get => _selectedQuest;
                    set { if (_selectedQuest != value) { _selectedQuest = value; OnPropertyChanged("SelectedQuest"); } }
                }*/

        public CombatViewModel()
        {
            _playerAttackedCommand = new DelegateCommand(OnPlayerAttacked);
            _enemyAttackedCommand = new DelegateCommand(OnEnemyAttacked);
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

        public void OnEnemyAttacked(object parameter)
        {
            TakeTurnEventArgs turnEvent = new(_activeCombat.Player, _activeCombat.Enemy, (int)parameter);
           
            _activeCombat.HandleTurnEvent(turnEvent);
            OnPropertyChanged("PlayerHealth");
            OnPropertyChanged("EnemyHealth");
        }
        public void OnPlayerAttacked(object parameter)
        {
            TakeTurnEventArgs turnEvent = new(_activeCombat.Enemy, _activeCombat.Player, (int)parameter);
            _activeCombat.HandleTurnEvent(turnEvent);
            OnPropertyChanged("PlayerHealth");
            OnPropertyChanged("EnemyHealth");
        }
    }
}


