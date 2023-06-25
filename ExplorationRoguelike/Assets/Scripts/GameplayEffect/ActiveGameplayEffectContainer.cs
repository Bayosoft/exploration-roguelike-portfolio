using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    //  A container with accessors specific to active gameplay effects, such as telling it to remove effects by application of a given effect
    public class ActiveGameplayEffectContainer
    {
        public List<ActiveGameplayEffect> ActiveEffects { get; private set; }
        private AbilitySystemComponent _owner;

        public void Initialize(AbilitySystemComponent owner)
        {
            _owner = owner;
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpec(GameplayEffectSpecification effectSpec)
        {
            ActiveGameplayEffect effect = new ActiveGameplayEffect(effectSpec);

            return effect.Handle;
        }

        public void RemoveGameplayEffectsWithAssetTags(GameplayTagContainer tags)
        {
            ActiveEffects.RemoveAll(effect => { return effect.Specification.EffectSO.AssetTags.HasAny(tags); });
        }
    }
}
