using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using Godot;
using System.Collections.Generic;

namespace ExplorationRoguelike.Combat
{
    [GlobalClass]
    public abstract partial class TurnComponent : Node
    {
        public bool MyTurn { get; protected set; } 

        public EventResource endTurnEvent;
        public abstract void StartTurn();
        public abstract void Act(GameplayAbility action, List<Character> targets);
        public abstract void EndTurn();

        private AbilitySystemComponent _owner;

        public AbilitySystemComponent Owner 
        {
            get
            {
                if (_owner == null)
                {
                    // TODO: Godotify? Not needed probably with better architecture.
                   // _owner = gameObject.GetComponent<AbilitySystemComponent>();
                } 
                return _owner;
            }
        }
    }
}
