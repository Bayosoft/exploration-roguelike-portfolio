using ExplorationRoguelike;
using UnityEngine;

[CreateAssetMenu(fileName = "Goblin", menuName = "ScriptableObjects/Enemies/Goblin", order = 1)]
public class GoblinCharacterSO : CharacterSO
{
    // Unique racial properties that isnt shared among other races.


    public int RegenerationBonus = 2;

}
