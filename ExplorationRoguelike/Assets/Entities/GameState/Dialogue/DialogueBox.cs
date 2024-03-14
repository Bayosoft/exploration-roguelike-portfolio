using System;
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
    }    
}