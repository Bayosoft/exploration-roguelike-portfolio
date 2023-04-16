using ExplorationRoguelike;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "ScriptableObjects/Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    [field: SerializeField]
    public EnemyTypeEnum EnemyType { get; set; }
}