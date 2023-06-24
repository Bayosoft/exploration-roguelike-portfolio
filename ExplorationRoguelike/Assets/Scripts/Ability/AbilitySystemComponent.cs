using ExplorationRoguelike;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        [SerializeField]
        private Character _owner;
        public List<AbilityData> GrantedAbilities { get => _owner.CharacterData.Abilities; }
        public GameplayTagContainer ActiveGameplayTags { get; }
        public ActiveGameplayEffectContainer ActiveGameplayEffectContainer { get; private set; }
        public bool TryActivateAbility(AbilityData ability, IEnumerable<AbilitySystemComponent> targets)
        {
            /*if(!CanActivateAbility(ability, ))
              {
                return false;
             }*/

            ability.Activate(this, targets);

            return true;
        }

        public bool CanActivateAbility(Character target, AbilityData ability)
        {
            // Logic to see if the ability can be used

            return false;
        }

        public float CalculateAggregatedModifiers(ref float value, in GameplayTagContainer valueTags, in GameplayTagContainer dynamicTags)
        {
            foreach (ActiveGameplayEffect activeEffect in ActiveGameplayEffectContainer.ActiveGameplayEffects)
            {
                GameplayEffect effect = activeEffect.Specification.GameplayEffect;

                if (!MeetsTagRequirements(effect))
                {
                    continue;
                }

                foreach (Modifier modifier in effect.Modifiers)
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

        public ActiveGameplayEffectHandle ApplyGameplayEffectToTarget(GameplayEffect effect, AbilitySystemComponent? target)
        {
            // Make a gameplay effect spec
            //  return ApplyGameplayEffectSpecToTarget(MakeOutgoingSpec(), target);
            return null;
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpecToTarget(GameplayEffectSpecification effectSpec, AbilitySystemComponent? target)
        {
            if (target != null)
            {
                return target.ApplyGameplayEffectSpecToSelf(effectSpec);
            }
            return null;
            // return ActiveGameplayEffectHandle();
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectToSelf(GameplayEffect effect)
        {
            //  GameplayEffectSpec effectSpec = GameplayEffectSpec(effect, context);
            // return ApplyGameplayEffectSpecToSelf(effectSpec);
            return null;
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpecToSelf(GameplayEffectSpecification effectSpec)
        {
            // Actually apply the gameplay effect spec
            return null;
        }

        public GameplayEffectSpecification MakeOutgoingSpec(GameplayEffect effect, float level, GameplayEffectContext? context)
        {
            /*            if (!context.IsValid())
                        {
                            context = MakeEffectContext();
                        }

                        if (effect)
                        {
                            return GameplayEffectSpec(effect, context, level);
                        }
                        return GameplayEffectSpec();
            */
            return null;
        }

        public GameplayEffectContext MakeEffectContext()
        {
            // Get context from ASC including instigator
            return null;
        }
    }
}