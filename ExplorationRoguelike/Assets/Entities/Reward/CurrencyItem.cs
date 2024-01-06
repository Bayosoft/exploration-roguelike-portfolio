using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class CurrencyItem : LootItem
    {
        public enum CurrencyType
        {
            Gold
        }

        private int _amount;
        private CurrencyType _currencyType;
        public void Initialize(int amount, CurrencyType currencyType, Sprite sprite)
        {
            _amount = amount;
            _currencyType = currencyType;
            ItemName.text = $"{amount} {currencyType}";
            Image.sprite = sprite;
        }
        public override void OnClaimReward()
        {
           onClaimRewardEvent.RaiseEvent(new OnClaimCurrencyRewardEventArgs(_amount, _currencyType));
        }
    }
}
