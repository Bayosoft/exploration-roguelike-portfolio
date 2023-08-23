using UnityEngine;
using UnityEngine.Events;

namespace ExplorationRoguelike.Scripts
{
    public class EventListener : MonoBehaviour 
    {
        public ScriptableEvent @event;
        public UnityEvent<ConcreteEventArgs> onArgsEventTriggered;
        public UnityEvent onEventTriggered;

        void OnEnable()
        {
            @event.AddListener(this);
        }
        void OnDisable()
        {
            @event.RemoveListener(this);
        }
        public void RaiseEvent(ConcreteEventArgs args)
        {
            onArgsEventTriggered.Invoke(args);
        }

        public void RaiseEvent()
        {
            onEventTriggered.Invoke();
        }
    }
}
