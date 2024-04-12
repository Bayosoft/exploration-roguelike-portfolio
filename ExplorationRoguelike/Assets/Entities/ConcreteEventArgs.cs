using System;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ConcreteEventArgs : EventArgs
    {
        public TEventArgs ValidateEventArgs<TEventArgs>(object caller = null)
        {
            if(this is TEventArgs eventArgsType)
            {
                return eventArgsType;
            }
            else
            {
                Debug.LogError($"EventArgs on {caller} is not of expected type {typeof(TEventArgs)}");
                throw new InvalidCastException();
            }
        }

        public bool TryValidateEventArgs<TEventArgs>(out TEventArgs validatedEvent) 
        {
            if (this is TEventArgs eventArgsType)
            {
                validatedEvent = eventArgsType;
                return true;
            }
            else
            {
                validatedEvent = default;
                return false;
            }
        }
    }
}
