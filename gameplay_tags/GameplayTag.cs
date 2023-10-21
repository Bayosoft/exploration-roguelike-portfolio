using Godot;

namespace ExplorationRoguelike.GameplayTags
{
    public partial class GameplayTag : Resource
    {
        public GameplayTag parent;

        public bool Matches(GameplayTag tagToCompare)
        {
            return tagToCompare == this || (parent != null && parent.Matches(tagToCompare));
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
