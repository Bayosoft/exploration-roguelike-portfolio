using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ConcreteEventArgs : EventArgs
    {
        public TEventArgs ValidateEventArgs<TEventArgs>(EventArgs eventArgs, object caller = null)
        {
            if(eventArgs is TEventArgs eventArgsType)
            {
                return eventArgsType;
            }
            else
            {
                Debug.LogError($"EventArgs on {caller} is not of expected type {typeof(TEventArgs)}");
                throw new InvalidCastException();
            }
        }
    }
}
