using ExplorationRoguelike;
using System;
using UnityEngine;

public class Player : Character, ICombatant
{
    [SerializeField]
    private HealthComponent _healthComponent;
    public HealthComponent HealthComponent { get => _healthComponent; }

}
