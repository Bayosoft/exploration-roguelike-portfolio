using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [Serializable]
    public class DialogueBox
    {
        public int order;
        public SpeakerName speakerName;
        [TextArea(15, 20)]
        public string dialogue;

        [SerializeField]
        private List<DialogueOption> dialogueOptions;

        public List<DialogueOption> DialogueOptions => dialogueOptions;
    }    
}