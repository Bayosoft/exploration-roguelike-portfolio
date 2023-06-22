using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class GameplayTagContainer
    {
        [SerializeField]
        private List<GameplayTag> _tags;

        public bool HasAll()
        {
            if (_tags != null || _tags.Count != 0)
            {
                foreach (GameplayTag tag in _tags)
                {
                    if (!HasTag(tag))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool HasTag(GameplayTag tagToCheck)
        {
            foreach (GameplayTag tag in _tags)
            {
                return tag.Matches(tagToCheck);
            }
            return false;
        }

        // For modifying 
        void Add(GameplayTag Tag)
void Remove(GameplayTag Tag)
// Maybe also add/remove taking in containers

bool IsEmpty()
int Num()

// Single tags
bool HasTag(GameplayTag Tag)
bool HasTagExact(GameplayTag Tag)

// Has every given tag
bool HasAll(GameplayTagContainer Tags)
bool HasAllExact(GameplayTagContainer Tags)

// Has at least one of the given tags
bool HasAny(GameplayTagContainer Tags)
bool HasAnyExact(GameplayContainer Tags)
    }
}
