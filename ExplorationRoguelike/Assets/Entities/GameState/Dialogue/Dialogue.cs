using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue Text")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] 
        private List<DialogueBox> dialogueBoxes;

        public List<DialogueBox> DialogueBoxes => dialogueBoxes;

        [SerializeField]
        private List<DialogueOption> dialogueOptions;

        public List<DialogueOption> DialogueOptions => dialogueOptions;
    }
}