using ExplorationRoguelike;
using UnityEngine;

public class Player : Character, IPlayerCombatant
{
    [SerializeField]
    private HealthComponent _healthComponent;
    public HealthComponent HealthComponent  => _healthComponent;

    [SerializeField]
    private TurnComponent<CardDeckComponent> _turnComponent;
    public TurnComponent<CardDeckComponent> TurnComponent => _turnComponent;
}
