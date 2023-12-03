using Godot;
using System;
using System.Collections.Generic;

namespace ExplorationRoguelike
{
    [GlobalClass]
    public partial class EventResource : Resource
    {
        [Signal]
        public delegate void SignalEventHandler();

        [Signal]
        public delegate void SignalWithArgumentEventHandler(ConcreteEventArgs eventArgs);

        public void RaiseEvent(ConcreteEventArgs eventArgs)
        {
            EmitSignal(SignalName.SignalWithArgument, eventArgs);
        }

        public void RaiseEvent()
        {
            EmitSignal(SignalName.Signal);
        }
    }
}
