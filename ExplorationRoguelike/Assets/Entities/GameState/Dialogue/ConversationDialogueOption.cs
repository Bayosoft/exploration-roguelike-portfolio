using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "ConversationDialogueOption", menuName = "Dialogue/Conversation Option")]
    public class ConversationDialogueOption : DialogueOption
    {
        [SerializeField]
        private DialogueBox dialogue;

        public void SetNextDialogueBox(DialogueBox next)
        {
            dialogue = next;
        }

        public override void Activate()
        {

        }
    }    
}