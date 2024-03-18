using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DialogueStateComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueDisplay;

        [SerializeField]
        private ConversationDialogueOption nextOption;

        [SerializeField]
        private GameObject dialogueOptionButton;

        [SerializeField]
        private GameObject dialogueFrame;

        [SerializeField]
        private GameObject dialogueOptionsFrame;

        private Dialogue _dialogue;

        // TODO: Create Vertical (for dialogue order) and horizontal (for side) layout group for dialogue. And vertical layout group for options.
        public void Initialize(Dialogue dialogue)
        {
            _dialogue = dialogue;

            RenderDialogue(dialogue.DialogueBoxes.First());

            // TIP: Refer to how it is done in Lootbag with loot items
            // Set name
            // Set side
            // Set order
        }

        // TODO: This needs to run when a ConversationDialogueOption is Activated.
        public void OnNextDialogue(DialogueBox nextDialogue)
        {
            foreach (GameObject option in dialogueOptionsFrame.transform)
            {
                Destroy(option);
            }

            RenderDialogue(nextDialogue);
        }

        private void RenderDialogue(DialogueBox dialogueBox)
        {
            var dialogueDisplayInstance = Instantiate(dialogueDisplay);
            dialogueDisplayInstance.transform.SetParent(dialogueFrame.transform);

            dialogueDisplayInstance.GetComponent<DialogueDisplay>().DisplayDialogue(dialogueBox.dialogue);

            // If there is no option to choose
            if (dialogueBox.DialogueOptions == null || !dialogueBox.DialogueOptions.Any())
            {
                // If there is no next dialogue box
                if (!_dialogue.HasNext(dialogueBox))
                {
                    // TODO: Add close dialogue option (make CloseDialogueOption class where Activate closes scene)
                    return;
                }

                // else add next button which loads next dialogue box when clicked. 
                // TODO: Maybe not a button but just clicking the screen in general?
                nextOption.SetNextDialogueBox(_dialogue.GetNext(dialogueBox));

                RenderOption(nextOption);

                return;
            }

            // Else render dialogue options
            foreach (DialogueOption dialogueOption in dialogueBox.DialogueOptions)
            {
                RenderOption(dialogueOption);
            }
        }

        private void RenderOption(DialogueOption option)
        {
            var buttonInstance = Instantiate(dialogueOptionButton);
            buttonInstance.transform.SetParent(dialogueOptionsFrame.transform);

            buttonInstance.GetComponent<DialogueOptionButton>().Initialize(option);
        }
    }
}
