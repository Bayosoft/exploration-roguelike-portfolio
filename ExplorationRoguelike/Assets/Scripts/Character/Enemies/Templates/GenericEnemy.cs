using ExplorationRoguelike;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic Enemy", menuName = "ScriptableObjects/Enemies/Generic", order = 1)]
public class Enemy : CharacterData
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public string Description { get; set; }
    [field: SerializeField]
    public EnemyTypeEnum EnemyType { get; set; }
}