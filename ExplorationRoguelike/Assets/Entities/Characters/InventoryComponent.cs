using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class InventoryComponent : MonoBehaviour
    {
        [SerializeField]
        private InventoryData inventoryData;
        public InventoryData InventoryData => inventoryData;

        [SerializeField]
        private AbilitySystemComponent abilitySystemComponent;

        public event EventHandler<int> OnGoldChanged;

        public void Start()
        {
            if(InventoryData.Artifacts == null)
            {
                InventoryData.Artifacts = new ObservableCollection<Artifact>();
            }
        }
        public void OnItemObtained(ConcreteEventArgs eventArgs)
        {
            if (eventArgs.TryValidateEventArgs(out OnObtainCurrencyRewardEventArgs currencyEventArgs))
            {
                inventoryData.AddGold(currencyEventArgs.Amount);
                OnGoldChanged?.Invoke(this, inventoryData.Gold);
            }
            else if(eventArgs.TryValidateEventArgs(out OnObtainArtifactRewardEventArgs artifactEventArgs))
            {
                var spec = abilitySystemComponent.MakeOutgoingEffectSpec(artifactEventArgs.Artifact.GameplayEffect);
                abilitySystemComponent.ApplyGameplayEffectSpecToSelf(spec);

                inventoryData.Artifacts.Add(artifactEventArgs.Artifact);
            }
        }

        public int GetGold()
        {
            return inventoryData.Gold;
        }
    }
}
