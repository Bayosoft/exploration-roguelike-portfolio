using UnityEngine;

namespace ExplorationRoguelike.GameplayTags
{
    [CreateAssetMenu(fileName = "Tag", menuName = "Tag", order = 1)]
    public class GameplayTag : ScriptableObject
    {
        public GameplayTag parent;

        public bool Matches(GameplayTag tagToCompare)
        {
            return tagToCompare == this || (parent && parent.Matches(tagToCompare));
        }

        public bool MatchesExact(GameplayTag tagToCompare)
        {
            return tagToCompare == this;
        }

        public GameplayTagContainer ToSingleTagContainer()
        {
            return new GameplayTagContainer(this);
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
