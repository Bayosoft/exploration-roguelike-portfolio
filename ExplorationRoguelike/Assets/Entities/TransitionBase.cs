using JetBrains.Annotations;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class TransitionBase : ScriptableObject
    {
        // Base for transitions to different states
        // Should be used for anything that transitions to another state like exploration tiles, dialogue options, winning combat, and tavern stuff.

        [SerializeField, CanBeNull]
        protected string sceneName;

        public virtual void Awake()
        {
        }
    }
}
