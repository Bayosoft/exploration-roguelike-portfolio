using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExplorationRoguelike.CurrencyItem;

namespace ExplorationRoguelike
{
    public class OnClaimCurrencyRewardEventArgs : ConcreteEventArgs
    {
        public int Amount { get; }
        public CurrencyType CurrencyType { get; }

        public OnClaimCurrencyRewardEventArgs(int amount, CurrencyType currencyType)
        {
            Amount = amount;
            CurrencyType = currencyType;
        }

    }
}
