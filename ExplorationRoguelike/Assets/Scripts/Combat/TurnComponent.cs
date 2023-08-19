using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Combat
{
    public abstract class TurnComponent : MonoBehaviour
    {
        public bool MyTurn { get; set; } = false;

        public ScriptableEvent EndTurnEvent;
        public abstract void StartTurn();
        public abstract void Act(GameplayAbility action, List<AbilitySystemComponent> targets);
        public abstract void EndTurn();
    }
}
