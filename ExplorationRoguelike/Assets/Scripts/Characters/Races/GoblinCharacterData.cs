using ExplorationRoguelike;
using UnityEngine;

namespace ExplorationRoguelike.Characters.Races
{
    [CreateAssetMenu(fileName = "Goblin", menuName = "Characters/Races/Goblin")]
    public class GoblinCharacterData : CharacterData
    {
        // Unique racial properties that isnt shared among other races.
        public int regenerationBonus = 2;
    }
}

