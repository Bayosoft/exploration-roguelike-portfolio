using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExplorationRoguelike
{
    public class DialogueDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dialogueText;

        [SerializeField]
        private GameObject dialogueBubble;

        [SerializeField]
        private GameObject emptyFill;
        public void DisplayDialogue(DialogueBox dialogueBox)
        {
            if(dialogueBox.speakerName == SpeakerName.PLAYER)
            {
                emptyFill.transform.SetSiblingIndex(1);
                dialogueBubble.transform.localPosition = new Vector2(dialogueBubble.transform.localPosition.x - 50, dialogueBubble.transform.localPosition.y);
            }
            else
            {
                emptyFill.transform.SetSiblingIndex(0);
                dialogueBubble.transform.localPosition = new Vector2(dialogueBubble.transform.localPosition.x + 50, dialogueBubble.transform.localPosition.y);
            }
            dialogueText.text = dialogueBox.dialogue;
        }
    }
}
