using ExplorationRoguelike.GameplayTags;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Scripts.GameplayTags
{
    [Serializable]
    public class GameplayTagContainer : IEnumerable
    {
        [SerializeField]
        private List<GameplayTag> tags;

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
            if(tags == null)
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
            if(tagsToCheck != null)
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
            if(tagsToCheck != null)
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
            if(tagToCheck != null)
            {
                foreach (GameplayTag tag in tags)
                {
                    if(tag.Matches(tagToCheck))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasTagExact(GameplayTag tagToCheck)
        {
            if(tagToCheck != null)
            {
                return tags.Contains(tagToCheck);
            }

            return false;
        }

        public void Add(GameplayTag tag)
        {
            if(tag != null)
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

    [Serializable]
    public struct GameplayTagRequirements
    {
        public GameplayTagContainer requiredTags;
        public GameplayTagContainer blockingTags;

        public bool HasRequirements()
        {
            return requiredTags.IsEmpty() && blockingTags.IsEmpty();
        }

        public bool RequirementsMet(GameplayTagContainer tags)
        {
            if(tags == null)
            {
                return requiredTags.IsEmpty();
            }

            return tags.HasAll(requiredTags) && !tags.HasAny(blockingTags);
        }

        public bool RequirementsMet(GameplayTagContainer tags, GameplayTagContainer dynamicTags)
        {
            if(tags == null)
            {
                return false;
            }

            foreach (GameplayTag requiredTag in requiredTags)
            {
                if (!tags.HasTag(requiredTag) && !dynamicTags.HasTag(requiredTag))
                {
                    return false;
                }
            }

            if (tags.HasAny(blockingTags) || dynamicTags.HasAny(blockingTags))
            {
                return false;
            }

            return true;
        }
    }
}
