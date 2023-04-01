using System.ComponentModel;
using System;
using ExplortationRoguelike.GUI.Combat;
using UnityEngine;
using ExplorationRoguelike.Scripts.Combat;
using static UnityEditor.Rendering.CameraUI;

namespace ExplortationRoguelike.GUI.Combat
{
    public class CombatViewModel : ViewModel
    {
        private ActiveCombat _activeCombat;

        public int PlayerHealth { get {  return _activeCombat.Player.CurrentHealth; } }
        public int EnemyHealth { get { return _activeCombat.Enemy.CurrentHealth; } }

        [SerializeField]
        private NoesisEventCommand _enemyAttackedCommand;
        public NoesisEventCommand EnemyAttackedCommand { get => _enemyAttackedCommand; }

        /*        private Quest _selectedQuest;
                public Quest SelectedQuest
                {
                    get => _selectedQuest;
                    set { if (_selectedQuest != value) { _selectedQuest = value; OnPropertyChanged("SelectedQuest"); } }
                }*/

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

        public void OnEnemyAttacked()
        {
            _activeCombat.StartTurn();
            OnPropertyChanged("PlayerHealth");
            OnPropertyChanged("EnemyHealth");
        }

    }
}


