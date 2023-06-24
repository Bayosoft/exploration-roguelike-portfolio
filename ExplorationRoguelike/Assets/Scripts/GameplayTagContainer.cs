using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [Serializable]
    public class GameplayTagContainer
    {
        [SerializeField]
        private List<GameplayTag> _tags;

        public List<GameplayTag> Tags { get { return _tags; } }

        public bool HasAll(GameplayTagContainer tagsToCheck)
        {
            if (_tags != null || _tags.Count != 0)
            {
                foreach (GameplayTag tagToCheck in tagsToCheck.Tags)
                {
                    if (!HasTag(tagToCheck))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool HasAllExact(GameplayTagContainer tagsToCheck)
        {
            if (_tags != null || _tags.Count != 0)
            {
                foreach (GameplayTag tagToCheck in tagsToCheck.Tags)
                {
                    if (!HasTagExact(tagToCheck))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool HasAny(GameplayTagContainer tagsToCheck)
        {
            if (_tags != null || _tags.Count != 0)
            {
                foreach (GameplayTag tagToCheck in tagsToCheck.Tags)
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
                foreach (GameplayTag tagToCheck in tagsToCheck.Tags)
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
            foreach (GameplayTag tag in _tags)
            {
                return tag.Matches(tagToCheck);
            }
            return false;
        }

        public bool HasTagExact(GameplayTag tagToCheck)
        {
            foreach (GameplayTag tag in _tags)
            {
                return tag.MatchesExact(tagToCheck);
            }
            return false;
        }

        public void Add(GameplayTag tag)
        {
            _tags.Add(tag);
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
            _tags.Remove(tag);
        }

        public bool IsEmpty()
        {
            return _tags.Count == 0;
        }
    }
}
