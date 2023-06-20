using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Tag", menuName = "Tag", order = 1)]
    public class GameplayTag : ScriptableObject
    {
        public GameplayTag Parent;
    }
}
