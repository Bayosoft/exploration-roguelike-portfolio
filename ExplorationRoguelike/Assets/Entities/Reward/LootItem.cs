using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExplorationRoguelike
{
    public class LootItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI itemName;

        [SerializeField]
        private Image image;

        public void Initialize(string name, Sprite sprite)
        {
            itemName.text = name;
            image.sprite = sprite;
        }
    }
}
