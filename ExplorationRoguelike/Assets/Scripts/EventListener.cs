using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ExplorationRoguelike
{
    public class EventListener : MonoBehaviour 
    {
        public ScriptableEvent Event;
        public UnityEvent<ConcreteEventArgs> onArgsEventTriggered;
        public UnityEvent onEventTriggered;

        void OnEnable()
        {
            Event.AddListener(this);
        }
        void OnDisable()
        {
            Event.RemoveListener(this);
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
