using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DialogueBox : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dialogueText;

        public void DisplayDialogue(string dialogue)
        {
            dialogueText.text = dialogue;
        }
    }
}
