using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExplorationRoguelike.CurrencyItem;

namespace ExplorationRoguelike
{
    public class OnObtainCurrencyRewardEventArgs : ConcreteEventArgs
    {
        public int Amount { get; }
        public CurrencyType CurrencyType { get; }

        public OnObtainCurrencyRewardEventArgs(int amount, CurrencyType currencyType)
        {
            Amount = amount;
            CurrencyType = currencyType;
        }

    }
}
