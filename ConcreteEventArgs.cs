using Godot;
using System;
namespace ExplorationRoguelike
{
    public partial class ConcreteEventArgs : GodotObject
    {
        public TEventArgs ValidateEventArgs<TEventArgs>(object caller = null)
        {
            if(this is TEventArgs eventArgsType)
            {
                return eventArgsType;
            }
            else
            {
                GD.PrintErr($"EventArgs on {caller} is not of expected type {typeof(TEventArgs)}");
                throw new InvalidCastException();
            }
        }
    }
}
