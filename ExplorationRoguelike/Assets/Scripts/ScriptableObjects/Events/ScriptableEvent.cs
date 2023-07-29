using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(menuName = "Events/Event", fileName = "Event")]
    public class ScriptableEvent : ScriptableObject
    {
        private List<EventListener> listeners = new();
        public void RaiseEvent(ConcreteEventArgs eventArgs)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].RaiseEvent(eventArgs);
            }
        }

        public void RaiseEvent()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].RaiseEvent();
            }
        }
        public void AddListener(EventListener listener)
        {
            listeners.Add(listener);
        }
        public void RemoveListener(EventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
