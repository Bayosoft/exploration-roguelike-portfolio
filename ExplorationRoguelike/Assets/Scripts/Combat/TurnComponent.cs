using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
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
