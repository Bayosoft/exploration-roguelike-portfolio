using ExplorationRoguelike;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace ExplorationRoguelike
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        [SerializeField]
        private Character _owner;
        public List<GameplayAbility> GrantedAbilities { get => _owner.CharacterData.Abilities; }
        public GameplayTagContainer ActiveGameplayTags { get; }
        public ActiveGameplayEffectContainer ActiveGameplayEffects { get; private set; }

        public GameplayTagContainer OwnedGameplayTags { get; private set; }

        public void OnEnable()
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
            if(!CanActivateAbility(ability))
            {
                return false;
            }

            ability.Activate(this, targets);

            return true;
        }

        public bool CanActivateAbility(GameplayAbility ability)
        {
            // Logic to see if the ability can be used

            return false;
        }

        public float CalculateAggregatedModifiers(ref float value, in GameplayTagContainer valueTags, in GameplayTagContainer dynamicTags)
        {
            foreach (ActiveGameplayEffect activeEffect in ActiveGameplayEffects.ActiveEffects)
            {
                if (activeEffect.IsInhibited)
                {
                    continue;
                }

                foreach (Modifier modifier in activeEffect.Specification.EffectSO.Modifiers)
                {
                    modifier.TryApply(ref value, valueTags, dynamicTags);
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
            if(effect == null || target == null)
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
            if(effectSpec == null)
            {
                return null;
            }

            // Application tag requirements must be met to apply
            if (!effectSpec.EffectSO.ApplicationTagRequirements.RequirementsMet(OwnedGameplayTags))
            {
                return null;
            }

            // Cannot apply when removal tag requirements are met
            if(!effectSpec.EffectSO.RemovalTagRequirements.RequirementsMet(OwnedGameplayTags))
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

                if(effectSpec.EffectSO.IsPeriodic && effectSpec.EffectSO.ExecutePeriodicImmediately)
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
            return MakeOutgoingEffectSpec(effect, MakeOutgoingEffectContext());
        }
        public GameplayEffectSpecification MakeOutgoingEffectSpec(GameplayEffect effect, GameplayEffectContext context)
        {
            if (effect != null && context != null)
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