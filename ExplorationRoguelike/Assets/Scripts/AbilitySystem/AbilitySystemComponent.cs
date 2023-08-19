using ExplorationRoguelike;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.GameplayEffects;
using ExplorationRoguelike.Scripts.GameplayEffects;
using ExplorationRoguelike.Scripts.GameplayTags;
using System.Collections.Generic;
using UnityEngine;
namespace ExplorationRoguelike.AbilitySystem
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        [SerializeField]
        private Character _owner;
        public List<GameplayAbility> GrantedAbilities { get => _owner.CharacterData.Abilities; }
        public GameplayTagContainer ActiveGameplayTags { get; }
        public GameplayTagContainer OwnedGameplayTags { get; private set; }

        public ActiveGameplayEffectContainer ActiveGameplayEffects;

        AbilitySystemComponent()
        {
            ActiveGameplayTags = new();
            OwnedGameplayTags = new();
        }

        void Awake()
        {
            ActiveGameplayEffects = new(this);
        }

        // In case levels are a thing
        public int GetLevel()
        {
            return 1;
        }

        public bool TryActivateAbility(GameplayAbility ability, IEnumerable<AbilitySystemComponent> targets)
        {
            if (ability == null)
            {
                return false;
            }

            if (!CanActivateAbility(ability))
            {
                return false;
            }

            ability.Activate(this, targets);

            return true;
        }

        public bool CanActivateAbility(GameplayAbility ability)
        {
            // Logic to see if the ability can be used

            return true;
        }

        public float CalculateAggregatedModifiers(float value, in GameplayTagContainer valueTags, GameplayTagContainer dynamicTags = null)
        {
            foreach (ActiveGameplayEffect activeEffect in ActiveGameplayEffects)
            {
                if (activeEffect.IsInhibited)
                {
                    continue;
                }

                AbilitySystemComponent instigator = activeEffect.Specification.Context.Instigator;

                foreach (GameplayModifierSpec modifier in activeEffect.Specification.Modifiers)
                {
                    modifier.TryApply(ref value, valueTags, dynamicTags, instigator, this);
                }
            }

            return value;
        }

        public bool MeetsTagRequirements(GameplayEffect effect)
        {
            return HasAll(effect.ApplicationTagRequirements.RequiredTags) && !HasAny(effect.ApplicationTagRequirements.BlockingTags);
        }

        public bool HasAll(GameplayTagContainer tags)
        {
            return ActiveGameplayTags.HasAll(tags);
        }


        public bool HasAny(GameplayTagContainer tags)
        {
            return ActiveGameplayTags.HasAny(tags);
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectToTarget(GameplayEffect effect, ref AbilitySystemComponent target)
        {
            if (effect == null || target == null)
            {
                return new ActiveGameplayEffectHandle();
            }

            return ApplyGameplayEffectSpecToTarget(MakeOutgoingEffectSpec(effect), target);
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpecToTarget(GameplayEffectSpecification effectSpec, AbilitySystemComponent target)
        {
            if (effectSpec == null || target == null)
            {
                return new ActiveGameplayEffectHandle();
            }

            return target.ApplyGameplayEffectSpecToSelf(effectSpec);
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectToSelf(GameplayEffect effect)
        {
            //  GameplayEffectSpec effectSpec = GameplayEffectSpec(effect, context);
            return ApplyGameplayEffectSpecToSelf(MakeOutgoingEffectSpec(effect));
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpecToSelf(GameplayEffectSpecification effectSpec)
        {
            if (effectSpec == null)
            {
                return null;
            }

            // Application tag requirements must be met to apply
            if (!effectSpec.EffectSO.ApplicationTagRequirements.RequirementsMet(OwnedGameplayTags))
            {
                return null;
            }

            // Cannot apply when removal tag requirements are met
            if (!effectSpec.EffectSO.RemovalTagRequirements.RequirementsMet(OwnedGameplayTags))
            {
                return null;
            }

            ///@TODO: Check immunities

            ActiveGameplayEffectHandle appliedHandle = new ActiveGameplayEffectHandle(-1);

            if (effectSpec.EffectSO.DurationType == GameplayDurationType.Instant)
            {
                // Execute gameplay effect once
                ExecuteActiveGameplayEffect(effectSpec);
            }
            else
            {
                // Apply the effect to the active effects
                appliedHandle = ActiveGameplayEffects.ApplyGameplayEffectSpec(effectSpec);
                // If it's periodic and should execute periodics immediately, do so

                if (effectSpec.EffectSO.IsPeriodic && effectSpec.EffectSO.ExecutePeriodicImmediately)
                {
                    ExecuteActivePeriodicEffect(appliedHandle);
                }
            }

            RemoveGameplayEffectsWithAssetTags(OwnedGameplayTags);

            // Actually apply the gameplay effect spec
            return appliedHandle;
        }

        // Removes all active gameplay effects with given asset tags.
        public void RemoveGameplayEffectsWithAssetTags(GameplayTagContainer tags)
        {
            ActiveGameplayEffects.RemoveGameplayEffectsWithAssetTags(tags);
        }

        // Execute instant gameplay effect 
        public void ExecuteActiveGameplayEffect(GameplayEffectSpecification effectSpec)
        {
            // 1) Apply modifiers
            foreach (GameplayModifierSpec modifierSpec in effectSpec.Modifiers)
            {
                // TODO: Figure out how to execute modifiers to affect desired values (Health, resource, stats)
            }

            // 2) Apply executions
        }

        // Execute periodic tick of an active gameplay effect
        public void ExecuteActivePeriodicEffect(ActiveGameplayEffectHandle handle)
        {
            if (handle == null)
            {
                return;
            }
        }

        public GameplayEffectSpecification MakeOutgoingEffectSpec(GameplayEffect effect)
        {
            if (effect != null)
            {
                return MakeOutgoingEffectSpec(effect, MakeOutgoingEffectContext());
            }
            return null;
        }
        public GameplayEffectSpecification MakeOutgoingEffectSpec(GameplayEffect effect, GameplayEffectContext context)
        {
            if (effect != null)
            {
                return new(effect, context, GetLevel());
            }

            return null;
        }

        public GameplayEffectContext MakeOutgoingEffectContext()
        {
            // Get context from ASC including instigator
            GameplayEffectContext context = new(this);

            return context;
        }
    }
}