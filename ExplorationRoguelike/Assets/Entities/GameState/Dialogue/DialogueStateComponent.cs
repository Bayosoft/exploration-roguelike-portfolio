using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DialogueStateComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueDisplay;

        [SerializeField]
        private GameObject dialogueOptionButton;

        // TODO: Create Vertical (for dialogue order) and horizontal (for side) layout group for dialogue. And vertical layout group for options.
        public void Initialize(Dialogue dialogue)
        {
            foreach (DialogueBox dialogueBox in dialogue.DialogueBoxes)
            {
                var dialogueDisplayInstance = Instantiate(dialogueDisplay);
                dialogueDisplayInstance.transform.SetParent(transform);

                // TIP: Refer to how it is done in Lootbag with loot items
                // Set name
                // Set side
                // Set order
                dialogueDisplayInstance.GetComponent<DialogueDisplay>().DisplayDialogue(dialogueBox.dialogue);
            }

            foreach (DialogueOption dialogueOption in dialogue.DialogueOptions)
            {
                var buttonInstance = Instantiate(dialogueOptionButton);
                buttonInstance.transform.parent = transform;

                buttonInstance.GetComponent<DialogueOptionButton>().Initialize(dialogueOption);
            }

        }
    }
}
