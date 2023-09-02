using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Combat
{
    public abstract class TurnComponent : MonoBehaviour
    {
        public bool MyTurn { get; set; } = false;

        public ScriptableEvent endTurnEvent;
        public abstract void StartTurn();
        public abstract void Act(GameplayAbility action, List<AbilitySystemComponent> targets);
        public abstract void EndTurn();

        private AbilitySystemComponent _owner;

        public AbilitySystemComponent Owner 
        {
            get
            {
                if (_owner == null)
                {
                    _owner = gameObject.GetComponent<AbilitySystemComponent>();
                } 
                return _owner;
            }
        }
    }
}
