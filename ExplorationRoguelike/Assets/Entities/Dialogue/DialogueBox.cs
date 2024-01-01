using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DialogueBox : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dialogueText;
        // Start is called before the first frame update
        void Start()
        {
            dialogueText.text = "You're bananas!";
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
