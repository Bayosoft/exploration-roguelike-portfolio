using UnityEngine;

namespace ExplorationRoguelike
{
    public class CurrencyItem : ObtainableItem
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
        public override void OnObtain()
        {
            onObtainEvent.RaiseEvent(new OnObtainCurrencyRewardEventArgs(_amount, _currencyType));

            Destroy(gameObject);
        }
    }
}
