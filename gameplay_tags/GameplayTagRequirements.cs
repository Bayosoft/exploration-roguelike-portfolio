using Godot;
using System;

namespace ExplorationRoguelike.GameplayTags;

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
        if (tags == null)
        {
            return requiredTags.IsEmpty();
        }

        return tags.HasAll(requiredTags) && !tags.HasAny(blockingTags);
    }

    public bool RequirementsMet(GameplayTagContainer tags, GameplayTagContainer dynamicTags)
    {
        if (tags == null)
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