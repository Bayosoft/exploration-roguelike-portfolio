using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ExplorationRoguelike
{
    [Serializable]
    public enum GameplayDurationType
    {
        Instant,
        Duration,
        Infinite
    }

    public static class GameplayEffectConstants
    {
        public const float InfiniteDuration = -1f;
        public const float InstantApplication = 0f;
    }

    [CreateAssetMenu(fileName = "Gameplay Effect", menuName = "Gameplay Effect")]
    public class GameplayEffect : ScriptableObject
    {
        public GameplayDurationType DurationType;

        public int Duration; // Duration of the effect in turns
        public bool IsPeriodic; // Not relevant on instant effects

        public int PeriodicDelay; // Delay between periodic ticks in turns
        public bool ExecutePeriodicImmediately; // Whether to execute once before the first periodic delay

        // These 3 may differ depending on DurationPolicy and IsPeriodic
        // Instant applies immediately. Periodic apply them every periodic tick. 
        // Duration and infinite without periodic apply modifiers and conditional effects until this main effect ends. (Unless they're marked to persist longer individually)
        public List<Execution> Executions; // Select custom execution script, specify values to affect what's being sent in from the effect on application.
       
        public List<Modifier> Modifiers; // List of modifiers to apply. (For non-periodic and non-instant, this is applied while active and removed after)
        public List<ConditionalGameplayEffect> ConditionalEffects; // List of conditional effects to apply if this effect is applied and their individual tag requirements are met

        public List<ConditionalGameplayEffect> OnEarlyRemoval; // Effects applied if this effect ends before it expired.
        public List<ConditionalGameplayEffect> OnExpirationEffects; // Effects applied to target when this effect expires.

        [Header("Tags")]
        public GameplayTagContainer AssetTags; // Tags this effect has, but do not grant
        public GameplayTagContainer GrantedTags; // Grants these tags to the target
        public GameplayTagContainer GrantedBlockedAbilityTags; // Grants blocked ability tags to target, preventing activating abilities with any of these.
        public GameplayTagRequirements ApplicationTagRequirements; // Must pass these for target tags to apply to target
        public GameplayTagRequirements OngoingTagRequirements; // Ongoing target tag requirements, effect is inactive while met, but not removed.
        public GameplayTagRequirements RemovalTagRequirements; // Remove the effect when these are met
        public GameplayTagContainer RemoveEffectsWithTags; // Remove any effects with these tags

        public void Apply(AbilitySystemComponent target)
        {
         //   target.ActiveGameplayEffectContainer.ActiveGameplayEffects.Add(this);
        }
    }

    [Serializable]
    public struct GameplayTagRequirements
    {
        public GameplayTagContainer RequiredTags;
        public GameplayTagContainer BlockingTags;

        public bool RequirementsMet(GameplayTagContainer tags)
        {
            return tags.HasAll(RequiredTags) && !tags.HasAny(BlockingTags);
        }
    }

    [Serializable]
    public struct ConditionalGameplayEffect
    {
        public GameplayEffect GameplayEffect;
        public GameplayTagRequirements ApplicationTagRequirements;
        public bool IsPersistent;
    }

    [Serializable]
    public struct Modifier
    {
        [SerializeField]
        private GameplayTagRequirements _tagRequirements;

        // On the modifier
        // Return true if it passed the required tags to modify the value
        public bool TryApply(ref float value, in GameplayTagContainer valueTags, in GameplayTagContainer dynamicTags)
        {
            if (!_tagRequirements.RequiredTags.HasAll(valueTags) || !_tagRequirements.RequiredTags.HasAll(dynamicTags))
            {
                return false;
            }

            if (_tagRequirements.BlockingTags.HasAny(valueTags) || _tagRequirements.BlockingTags.HasAny(dynamicTags))
            {
                return false;
            }

            Apply(ref value);
            return true;
        }

        void Apply(ref float value)
        {
            // Modify the value
        }
    }

    public class Execution
    {

    }
}
