using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(menuName = "Game Event")]
    public class Event : ScriptableObject
    {
        private List<EventListener> listeners = new List<EventListener>();
        public void TriggerEvent()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventTriggered();
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
