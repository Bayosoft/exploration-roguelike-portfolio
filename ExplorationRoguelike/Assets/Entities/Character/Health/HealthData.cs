using System;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    [CreateAssetMenu(fileName = "Health", menuName = "Characters/Health")]
    public class HealthData : ScriptableObject
    {
        public int maxHealth;
    }
}
