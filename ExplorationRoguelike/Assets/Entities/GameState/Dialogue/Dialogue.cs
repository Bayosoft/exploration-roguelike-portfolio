using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue Text")]
    public class Dialogue : ScriptableObject
    {
        // Dictionary with a dictionary in it
        // Dictionary int order key, Dictionary (speaker key dialogue string value) value.
        [SerializeField] private SpeakerName speakerName;
        [SerializeField][TextArea(15, 20)]
        private string dialogue;

        [SerializeField] 
        private List<DialogueOption> dialogueOptions;
    }
}