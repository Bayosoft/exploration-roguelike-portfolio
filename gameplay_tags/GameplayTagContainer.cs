using Godot;
using Godot.Collections;
using System.Collections;

namespace ExplorationRoguelike.GameplayTags
{
    public partial class GameplayTagContainer : Resource, IEnumerable
    {
        [Export]
        private Array<GameplayTag> tags;

        public GameplayTagContainer()
        {
            tags = new();
        }

        public GameplayTagContainer(GameplayTag tag)
        {
            tags = new() { tag };
        }

        public bool HasAll(GameplayTagContainer tagsToCheck)
        {
            if (tags == null)
            {
                return tagsToCheck.IsEmpty();
            }

            foreach (GameplayTag tagToCheck in tagsToCheck)
            {
                if (!HasTag(tagToCheck))
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasAllExact(GameplayTagContainer tagsToCheck)
        {
            if (tags == null)
            {
                return tagsToCheck.IsEmpty();
            }

            foreach (GameplayTag tagToCheck in tagsToCheck)
            {
                if (!HasTagExact(tagToCheck))
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasAny(GameplayTagContainer tagsToCheck)
        {
            if (tagsToCheck != null)
            {
                foreach (GameplayTag tagToCheck in tagsToCheck)
                {
                    if (HasTag(tagToCheck))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasAnyExact(GameplayTagContainer tagsToCheck)
        {
            if (tagsToCheck != null)
            {
                foreach (GameplayTag tagToCheck in tagsToCheck)
                {
                    if (HasTagExact(tagToCheck))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasTag(GameplayTag tagToCheck)
        {
            if (tagToCheck != null)
            {
                foreach (GameplayTag tag in tags)
                {
                    if (tag.Matches(tagToCheck))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasTagExact(GameplayTag tagToCheck)
        {
            if (tagToCheck != null)
            {
                return tags.Contains(tagToCheck);
            }

            return false;
        }

        public void Add(GameplayTag tag)
        {
            if (tag != null)
            {
                tags.Add(tag);
            }
        }

        public void Remove(GameplayTag tag)
        {
            tags.Remove(tag);
        }

        public bool IsEmpty()
        {
            return tags.Count == 0;
        }

        public IEnumerator GetEnumerator()
        {
            return tags.GetEnumerator();
        }
    }
}
