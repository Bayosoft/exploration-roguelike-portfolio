using ExplorationRoguelike;
using UnityEngine;

public interface ICombatant<T> where T : MonoBehaviour
{ 
    public AbilitySystemComponent AbilitySystemComponent { get; }
    public HealthComponent HealthComponent { get; }
    public TurnComponent<T> TurnComponent { get; }
}