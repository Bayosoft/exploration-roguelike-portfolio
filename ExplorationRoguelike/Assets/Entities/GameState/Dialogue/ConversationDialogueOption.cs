using System;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "ConversationDialogueOption", menuName = "Dialogue/Conversation Option")]
    public class ConversationDialogueOption : DialogueOption
    {
        public event EventHandler<int> OptionSelected;

        [SerializeField]
        private int nextDialogueOrder;

        public void SetNextDialogueBox(int next)
        {
            nextDialogueOrder = next;
        }

        public override void Activate()
        {
            base.Activate();
            OptionSelected?.Invoke(this, nextDialogueOrder);
        }
    }    
}