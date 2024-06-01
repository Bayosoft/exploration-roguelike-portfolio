using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class PlayerGoldDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI goldText;
        public void Initialize(Player player)
        {
            UpdateGold(null, player.InventoryComponent.GetGold());
            player.InventoryComponent.OnGoldChanged += UpdateGold;
        }

        public void UpdateGold(object e, int amount)
        {
            goldText.text = $"{amount}";
        }
    }
}
