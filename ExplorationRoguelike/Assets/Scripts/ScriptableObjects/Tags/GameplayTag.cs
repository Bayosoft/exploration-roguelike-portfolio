using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Tag", menuName = "Tag", order = 1)]
    public class GameplayTag : ScriptableObject
    {
        public GameplayTag Parent;

        public bool Matches(GameplayTag tagToCompare)
        {
            return tagToCompare == this || (Parent && Parent.Matches(tagToCompare));
        }

        public bool MatchesExact(GameplayTag tagToCompare)
        {
            return tagToCompare == this;
        }

    }
}
