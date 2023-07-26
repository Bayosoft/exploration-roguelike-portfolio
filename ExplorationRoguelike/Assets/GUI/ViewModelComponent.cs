using ExplortationRoguelike.GUI.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ViewModelComponent : MonoBehaviour
    {
        public CombatStateComponent combatStateComponent;

        [SerializeField]
        private NoesisXaml _cardView;
        public static NoesisXaml CardXamlView;

        private void Start()
        {
            InitializeStaticViews();

            var viewModel = new CombatViewModel();
            viewModel.SetCombatStateComponent(combatStateComponent);

            GetComponent<NoesisView>().Content.DataContext = viewModel;

            viewModel.Start();         
        }

        private void InitializeStaticViews()
        {
            CardXamlView = _cardView;
        }
    }
}
