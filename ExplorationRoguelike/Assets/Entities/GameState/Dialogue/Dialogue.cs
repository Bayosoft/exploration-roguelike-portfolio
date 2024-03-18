using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] 
        private List<DialogueBox> dialogueBoxes;

        public List<DialogueBox> DialogueBoxes => dialogueBoxes;


        internal bool HasNext(DialogueBox current)
        {
            return DialogueBoxes.Count != DialogueBoxes.IndexOf(current);
        }

        internal DialogueBox GetNext(DialogueBox current)
        {
            return DialogueBoxes[DialogueBoxes.IndexOf(current) + 1];
        }
    }
}