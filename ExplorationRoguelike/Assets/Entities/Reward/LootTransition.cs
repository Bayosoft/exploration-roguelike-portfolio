using System;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "LootTransition", menuName = "Transitions/Loot Transition")]
    public class LootTransition : TransitionBase
    {
        [SerializeField]
        private GameObject lootBagPrefab;

        private Action nextButtonBehavior;
        private GameObject lootBagInstance;

        public override void Awake()
        {
        }

        public void Transition(LootTable lootTable, Action nextButtonBehavior)
        {
            this.nextButtonBehavior = nextButtonBehavior;

            lootBagInstance = Instantiate(lootBagPrefab);

            LootBag lootBag = lootBagInstance.GetComponent<LootBag>();
            lootBag.Initialize(lootTable);

            lootBag.nextButton.onClick.AddListener(NextButtonPressed);
        }

        private void NextButtonPressed()
        {
            nextButtonBehavior();
            Destroy(lootBagInstance);
        }
    }    
}