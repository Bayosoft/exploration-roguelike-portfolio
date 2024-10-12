using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExplorationRoguelike
{
    public abstract class ObtainableItem : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI ItemName;

        [SerializeField]
        public Image Image;

        [SerializeField]
        protected ScriptableEvent onObtainEvent;

        public abstract void OnObtain();
    }
}
