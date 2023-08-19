using ExplorationRoguelike.Scripts.GameplayTags;
using UnityEngine;

namespace ExplorationRoguelike.GameplayTags
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

        public GameplayTagContainer ToSingleTagContainer()
        {
            return new GameplayTagContainer(this);
        }
    }
}
