using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class InventoryComponent : MonoBehaviour
    {
        [SerializeField]
        private InventoryData inventoryData;

        public event EventHandler<int> OnGoldChanged;
        public void OnRewardClaimed(ConcreteEventArgs eventArgs)
        {
            var currencyEventArgs = eventArgs.ValidateEventArgs<OnClaimCurrencyRewardEventArgs>();

            inventoryData.AddGold(currencyEventArgs.Amount);
            OnGoldChanged?.Invoke(this, inventoryData.Gold);
        }

        public int GetGold()
        {
            return inventoryData.Gold;
        }
    }
}
