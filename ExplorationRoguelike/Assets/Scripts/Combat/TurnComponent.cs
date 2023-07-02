using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class TurnComponent<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool MyTurn { get; set; }
        public T CombatPlayComponent { get => GetComponent<T>(); }
        public abstract void Act(GameplayAbility action, List<AbilitySystemComponent> targets);
        public abstract void EndTurn();
    }
}
