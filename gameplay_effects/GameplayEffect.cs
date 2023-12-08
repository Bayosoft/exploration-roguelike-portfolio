using ExplorationRoguelike.AbilitySystem;
using System;
using System.Collections.Generic;
using ExplorationRoguelike.GameplayTags;
using Godot;
using Godot.Collections;

namespace ExplorationRoguelike.GameplayEffects
{
    [Serializable]
    public enum GameplayDurationType
    {
        Instant,
        Turns,
        Time,
        Infinite
    }

    public static class GameplayEffectConstants
    {
        public const int InfiniteDuration = -1;
        public const int InstantApplication = 0;
    }

    [GlobalClass]
    public partial class GameplayEffect : Resource
    {
        [Export] public string name;
        [Export] public string description; // Should be of same type as card description.

        [Export] public GameplayDurationType durationType;

        [Export] public int duration; // Duration of the effect in turns
        [Export] public bool isPeriodic; // Not relevant on instant effects

        [Export] public int periodicDelay; // Delay between periodic ticks in turns
        [Export] public bool executePeriodicImmediately; // Whether to execute once before the first periodic delay

        // These 3 may differ depending on DurationPolicy and IsPeriodic
        // Instant applies immediately. Periodic apply them every periodic tick. 
        // Duration and infinite without periodic apply modifiers and conditional effects until this main effect ends. (Unless they're marked to persist longer individually)
        public List<GameplayExecution> Executions; // Select custom execution script, specify values to affect what's being sent in from the effect on application.

        [Export] public Array<GameplayModifier> modifiers; // List of modifiers to apply. (For non-periodic and non-instant, this is applied while active and removed after)
        [Export] public Array<ConditionalGameplayEffect> conditionalEffects; // List of conditional effects to apply if this effect is applied and their individual tag requirements are met

        [Export] public Array<ConditionalGameplayEffect> earlyRemovalEffects; // Effects applied if this effect ends before it expired.
        [Export] public Array<ConditionalGameplayEffect> expirationEffects; // Effects applied to target when this effect expires.

        [ExportGroup("Tags")]
        public GameplayTagContainer assetTags; // Tags this effect has, but do not grant
        public GameplayTagContainer grantedTags; // Grants these tags to the target
        public GameplayTagContainer grantedBlockedAbilityTags; // Grants blocked ability tags to target, preventing activating abilities with any of these.
        public GameplayTagRequirements applicationTagRequirements; // Must pass these for target tags to apply to target
        public GameplayTagRequirements ongoingTagRequirements; // Ongoing target tag requirements, effect is inactive while met, but not removed.
        public GameplayTagRequirements removalTagRequirements; // Remove the effect when these are met
        public GameplayTagContainer removeEffectsWithTags; // Remove any effects with these tags
        
        private GameplayEffect()
        {
            durationType = GameplayDurationType.Instant;
            duration = 0;

            isPeriodic = false;
            periodicDelay = 0;
            executePeriodicImmediately = false;

            Executions = new List<GameplayExecution>();
            modifiers = new Array<GameplayModifier>();
            conditionalEffects = new Array<ConditionalGameplayEffect>();

            earlyRemovalEffects = new Array<ConditionalGameplayEffect>();
            expirationEffects = new Array<ConditionalGameplayEffect>();

            assetTags = new GameplayTagContainer();
            grantedTags = new GameplayTagContainer();
            grantedBlockedAbilityTags = new GameplayTagContainer();
            applicationTagRequirements = new GameplayTagRequirements();
            ongoingTagRequirements = new GameplayTagRequirements();
            removalTagRequirements = new GameplayTagRequirements();
            removeEffectsWithTags = new GameplayTagContainer();
        }

        public void Apply(AbilitySystemComponent target)
        {
         //   target.ActiveGameplayEffectContainer.ActiveGameplayEffects.Add(this);
        }
    }

    public partial class ConditionalGameplayEffect : GodotObject
    {
       [Export] public GameplayEffect gameplayEffect;
       [Export] public bool isPersistent;
       [Export] public GameplayTagRequirements applicationTagRequirements;
    }

    public class GameplayExecution
    {

    }
}
