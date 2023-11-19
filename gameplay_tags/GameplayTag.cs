using Godot;

namespace ExplorationRoguelike.GameplayTags
{
    [GlobalClass]
    public partial class GameplayTag : Resource
    {
        [Export]
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
