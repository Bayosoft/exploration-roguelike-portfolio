using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class MultiChoiceOptionData : ScriptableObject
    {
        public string OptionText;
        public abstract void Activate();
    }
}