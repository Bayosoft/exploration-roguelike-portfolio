using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class DialogueOptionButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dialogueOptionText;

        private DialogueOption _dialogueOption;

        public void Initialize(DialogueOption dialogueOption)
        {
            _dialogueOption = dialogueOption;

            dialogueOptionText.text = _dialogueOption.OptionText;
        }
        public void OnOptionClicked()
        {
            _dialogueOption.Activate();
        }
    }
}
