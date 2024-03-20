using System;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class DialogueOption : ScriptableObject
    {
        public string OptionText;
        public abstract void Activate();
    }
}