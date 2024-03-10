using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DialogueStateComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueBox;

        [SerializeField]
        private GameObject dialogueOptionButton;

        public void LoadDialogue(/* List<Dialogue> dialogues*/ List<DialogueOption> options)
        {
            foreach (DialogueOption option in options)
            {
                var buttonInstance = Instantiate(dialogueOptionButton);
                buttonInstance.transform.parent = transform;

                buttonInstance.GetComponent<DialogueOptionButton>().Initialize(option);
            }
        }
    }
}
