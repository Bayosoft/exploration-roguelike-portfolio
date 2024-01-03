using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DialogueStateComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject multiChoiceButton;

        public void LoadDialogue(/* List<Dialogue> dialogues*/ List<MultiChoiceOptionData> options)
        {
            foreach (MultiChoiceOptionData option in options)
            {
                var buttonInstance = Instantiate(multiChoiceButton);
                buttonInstance.transform.parent = transform;

                buttonInstance.GetComponent<MultiChoiceOption>().Initialize(option);
            }
        }
    }
}
