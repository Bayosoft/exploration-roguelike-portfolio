using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(menuName = "Events/Event", fileName = "Event")]
    public class ScriptableEvent : ScriptableObject
    {
        private List<EventListener> _listeners = new();
        public void RaiseEvent(ConcreteEventArgs eventArgs)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].RaiseEvent(eventArgs);
            }
        }

        public void RaiseEvent()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].RaiseEvent();
            }
        }
        public void AddListener(EventListener listener)
        {
            _listeners.Add(listener);
        }
        public void RemoveListener(EventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
