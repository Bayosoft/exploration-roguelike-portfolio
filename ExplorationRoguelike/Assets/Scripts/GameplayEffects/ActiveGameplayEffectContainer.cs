using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Scripts.GameplayTags;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike.GameplayEffects
{
    //  A container with accessors specific to active gameplay effects, such as telling it to remove effects by application of a given effect
    public class ActiveGameplayEffectContainer : IEnumerable
    {
        public ObservableCollection<ActiveGameplayEffect> ActiveEffects;
        private AbilitySystemComponent _owner;

        public ActiveGameplayEffectContainer(AbilitySystemComponent owner)
        {
            ActiveEffects = new();
            _owner = owner;
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpec(GameplayEffectSpecification effectSpec)
        {
            ActiveGameplayEffect effect = new ActiveGameplayEffect(effectSpec);

            ActiveGameplayEffect existingActiveEffect = GetActiveGameplayEffectByEffectSo(effectSpec.EffectSo);
            if(existingActiveEffect != null)
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
            foreach(ActiveGameplayEffect activeEffect in ActiveEffects.ToList())
            {
                if (activeEffect.Specification.EffectSo.assetTags.HasAny(tags))
                {
                    ActiveEffects.Remove(activeEffect);
                }
            }
        }

        public ActiveGameplayEffect GetActiveGameplayEffectByEffectSo(GameplayEffect effectSo)
        {
            foreach(ActiveGameplayEffect activeEffect in ActiveEffects)
            {
                if(activeEffect.Specification.EffectSo == effectSo)
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
