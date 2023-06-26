using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    //  A container with accessors specific to active gameplay effects, such as telling it to remove effects by application of a given effect
    public class ActiveGameplayEffectContainer : IEnumerable
    {
        private List<ActiveGameplayEffect> ActiveEffects;
        private AbilitySystemComponent _owner;

        public ActiveGameplayEffectContainer(AbilitySystemComponent owner)
        {
            ActiveEffects = new();
            _owner = owner;
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpec(GameplayEffectSpecification effectSpec)
        {
            ActiveGameplayEffect effect = new ActiveGameplayEffect(effectSpec);

            ActiveGameplayEffect ExistingActiveEffect = GetActiveGameplayEffectByEffectSO(effectSpec.EffectSO);
            if(ExistingActiveEffect != null)
            {
                
            }
            else
            {
                ActiveEffects.Add(effect);
            }


            return effect.Handle;
        }

        public void RemoveGameplayEffectsWithAssetTags(GameplayTagContainer tags)
        {
            ActiveEffects.RemoveAll(effect => { return effect.Specification.EffectSO.AssetTags.HasAny(tags); });
        }

        public ActiveGameplayEffect GetActiveGameplayEffectByEffectSO(GameplayEffect effectSO)
        {
            foreach(ActiveGameplayEffect activeEffect in ActiveEffects)
            {
                if(activeEffect.Specification.EffectSO == effectSO)
                {
                    return activeEffect;
                }
            }

            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return ActiveEffects.GetEnumerator();
        }
    }
}
