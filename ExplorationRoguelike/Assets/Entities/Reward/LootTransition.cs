using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class LootTransition : TransitionBase
    {
        [SerializeField]
        private GameObject lootBagPrefab;

        private Action nextButtonBehavior;
        private GameObject lootBagInstance;
        public static LootTransition Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
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
            nextButtonBehavior?.Invoke();
            Destroy(lootBagInstance);
        }
    }
}