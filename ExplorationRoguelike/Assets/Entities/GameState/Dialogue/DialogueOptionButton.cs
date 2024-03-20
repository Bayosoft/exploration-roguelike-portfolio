using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DialogueOptionButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dialogueOptionText;

        internal DialogueOption dialogueOption;

        public void Initialize(DialogueOption dialogueOption)
        {
            this.dialogueOption = dialogueOption;

            dialogueOptionText.text = this.dialogueOption.OptionText;
        }
        public void OnOptionClicked()
        {
            dialogueOption.Activate();
        }
    }
}
