using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [Serializable]
    public class GameplayTagContainer : IEnumerable
    {
        [SerializeField]
        private List<GameplayTag> _tags;

        GameplayTagContainer()
        {
            _tags = new();
        }

        public GameplayTagContainer(GameplayTag tag)
        {
            _tags = new() { tag };
        }

        public bool HasAll(GameplayTagContainer tagsToCheck)
        {
            if (_tags == null)
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
            if(_tags == null)
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
            if (_tags != null || _tags.Count != 0)
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
            if (_tags != null || _tags.Count != 0)
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
            if(tagToCheck == null)
            {
                return false;
            }

            foreach (GameplayTag tag in _tags)
            {
                return tag.Matches(tagToCheck);
            }
            return false;
        }

        public bool HasTagExact(GameplayTag tagToCheck)
        {
            if(tagToCheck == null)
            {
                return false;
            }

            return _tags.Contains(tagToCheck);
        }

        public void Add(GameplayTag tag)
        {
            if(tag != null)
            {
                _tags.Add(tag);
            }
        }

        public void Remove(GameplayTag tag)
        {
            if (HasTag(tag))
            {
                // TODO: Make it remove tag that has partial match.
                _tags.Remove(tag);
            }
        }

        public void RemoveExact(GameplayTag tag)
        {
            if(tag != null)
            {
                _tags.Remove(tag);
            }
        }

        public bool IsEmpty()
        {
            return _tags.Count == 0;
        }

        public IEnumerator GetEnumerator()
        {
            return _tags.GetEnumerator();
        }
    }

    [Serializable]
    public struct GameplayTagRequirements
    {
        public GameplayTagContainer RequiredTags;
        public GameplayTagContainer BlockingTags;

        public bool HasRequirements()
        {
            return RequiredTags.IsEmpty() && BlockingTags.IsEmpty();
        }

        public bool RequirementsMet(GameplayTagContainer tags)
        {
            return tags.HasAll(RequiredTags) && !tags.HasAny(BlockingTags);
        }

        public bool RequirementsMet(GameplayTagContainer tags, GameplayTagContainer dynamicTags)
        {
            foreach (GameplayTag requiredTag in RequiredTags)
            {
                if (!tags.HasTag(requiredTag) && !dynamicTags.HasTag(requiredTag))
                {
                    return false;
                }
            }

            if (tags.HasAny(BlockingTags) || dynamicTags.HasAny(BlockingTags))
            {
                return false;
            }

            return true;
        }
    }
}
