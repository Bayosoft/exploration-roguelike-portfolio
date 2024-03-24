using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ExplorationRoguelike
{
    public class DialogueStateComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueDisplayPrefab;

        [SerializeField]
        private ConversationDialogueOption nextOption;

        [SerializeField]
        private GameObject dialogueOptionButton;

        [SerializeField]
        private GameObject dialogueFrame;

        [SerializeField]
        private GameObject dialogueOptionsFrame;

        private Dialogue _dialogue;

        public void Initialize(Dialogue dialogue)
        {
            _dialogue = dialogue;

            RenderDialogue(dialogue.DialogueBoxes.First());

            // TIP: Refer to how it is done in Lootbag with loot items
            // Set name
            // Set side
            // Set order
        }

        // TODO: This needs to run when a ConversationDialogueOption is OptionSelected.
        public void OnOptionSelected(object sender, int currentDialogueOrder)
        {
            foreach (Transform option in dialogueOptionsFrame.transform)
            {
                if(option.gameObject.GetComponent<DialogueOptionButton>().dialogueOption is ConversationDialogueOption conversationDialogueOption)
                {
                    conversationDialogueOption.OptionSelected -= OnOptionSelected;
                }

                Destroy(option.gameObject);
            }

            var newDialogue = _dialogue.DialogueBoxes[currentDialogueOrder];

            RenderDialogue(newDialogue);
        }

        private void RenderDialogue(DialogueBox dialogueBox)
        {
            var dialogueDisplayInstance = Instantiate(dialogueDisplayPrefab);
            dialogueDisplayInstance.transform.SetParent(dialogueFrame.transform);

            var dialogueDisplay = dialogueDisplayInstance.GetComponent<DialogueDisplay>();

            dialogueDisplay.DisplayDialogue(dialogueBox);

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueDisplayInstance.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueDisplayInstance.GetComponentInChildren<RectTransform>());

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
                nextOption.SetNextDialogueBox(_dialogue.GetNextOrder(dialogueBox));

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

            var optionButton = buttonInstance.GetComponent<DialogueOptionButton>();

            optionButton.Initialize(option); 

            if(optionButton.dialogueOption is ConversationDialogueOption conversationDialogueOption)
            {
                conversationDialogueOption.OptionSelected += OnOptionSelected;
            }
        }
    }
}
