using ExplorationRoguelike;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.GameplayEffects;
using System.Collections.Generic;
using ExplorationRoguelike.GameplayTags;
using UnityEngine;
namespace ExplorationRoguelike.AbilitySystem
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        [SerializeField]
        private Character owner;
        public List<GameplayAbility> GrantedAbilities => owner.CharacterData.Abilities;
        public GameplayTagContainer ActiveGameplayTags { get; }
        public GameplayTagContainer OwnedGameplayTags { get; private set; }

        public ActiveGameplayEffectContainer ActiveGameplayEffects;

        private AbilitySystemComponent()
        {
            ActiveGameplayTags = new GameplayTagContainer();
            OwnedGameplayTags = new GameplayTagContainer();
        }

        private void Awake()
        {
            ActiveGameplayEffects = new ActiveGameplayEffectContainer(this);
        }

        // In case levels are a thing
        /*
        public int GetLevel()
        {
            return 1;
        }
        */

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

                var instigator = activeEffect.Specification.Instigator;

                foreach (var modifier in activeEffect.Specification.Modifiers)
                {
                    modifier.TryApply(ref value, valueTags, dynamicTags, instigator, this);
                }
            }

            return value;
        }

        public bool MeetsTagRequirements(GameplayEffect effect)
        {
            return HasAll(effect.applicationTagRequirements.requiredTags) && !HasAny(effect.applicationTagRequirements.blockingTags);
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
            if (!effectSpec.EffectSo.applicationTagRequirements.RequirementsMet(OwnedGameplayTags))
            {
                return null;
            }

            // Cannot apply when removal tag requirements are met
            if (!effectSpec.EffectSo.removalTagRequirements.RequirementsMet(OwnedGameplayTags))
            {
                return null;
            }

            ///@TODO: Check immunities

            ActiveGameplayEffectHandle appliedHandle = new ActiveGameplayEffectHandle(-1);

            if (effectSpec.EffectSo.durationType == GameplayDurationType.Instant)
            {
                // Execute gameplay effect once
                ExecuteActiveGameplayEffect(effectSpec);
            }
            else
            {
                // Apply the effect to the active effects
                appliedHandle = ActiveGameplayEffects.ApplyGameplayEffectSpec(effectSpec);
                // If it's periodic and should execute periodics immediately, do so

                if (effectSpec.EffectSo.isPeriodic && effectSpec.EffectSo.executePeriodicImmediately)
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
                return MakeOutgoingEffectSpec(effect, this);
            }
            return null;
        }
        public GameplayEffectSpecification MakeOutgoingEffectSpec(GameplayEffect effect, AbilitySystemComponent instigator)
        {
            if (effect != null)
            {
                return new GameplayEffectSpecification(effect, instigator/*, GetLevel()*/);
            }

            return null;
        }
    }
}