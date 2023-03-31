using ExplorationRoguelike.Scripts.Combat;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHealth;
    public int Damage;

    [SerializeField]
    private int _currentHealth;
    public int CurrentHealth { get => _currentHealth; }

    void Start()
    {
        _currentHealth = MaxHealth;
    }
    public void EnterCombat(ActiveCombat combat)
    {
        combat.Attacked += ReceiveAttack;
    }

    private void ReceiveAttack(object sender, EventArgs e)
    {
        _currentHealth -= 5; // TODO: Add actual damage/effect here from the event.
    }
}
