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
        public UnityEvent<ConcreteEventArgs> onEventTriggered;
        void OnEnable()
        {
            Event.AddListener(this);
        }
        void OnDisable()
        {
            Event.RemoveListener(this);
        }
        public void OnEventTriggered(ConcreteEventArgs args)
        {
            onEventTriggered.Invoke(args);
        }
    }
}
