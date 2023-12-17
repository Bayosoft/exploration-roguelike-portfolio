using ExplorationRoguelike.AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ExplorationRoguelike.Combat.Events;
using ExplorationRoguelike.GameplayTags;
using UnityEngine;

namespace ExplorationRoguelike.GameplayEffects
{
    //  A container with accessors specific to active gameplay effects, such as telling it to remove effects by application of a given effect
    public class ActiveGameplayEffectContainer : IEnumerable
    {
        public ObservableCollection<ActiveGameplayEffect> ActiveEffects;
        private readonly AbilitySystemComponent _owner;

        public ActiveGameplayEffectContainer(AbilitySystemComponent owner)
        {
            ActiveEffects = new ObservableCollection<ActiveGameplayEffect>();
            _owner = owner;
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpec(GameplayEffectSpecification effectSpec)
        {
            var effect = new ActiveGameplayEffect(effectSpec);

            var existingActiveEffect = GetActiveGameplayEffectByEffectSo(effectSpec.EffectSo);
            
            if(existingActiveEffect != null) // Replace effect. (Probably need to add logic for stackable effects)
            {
                ActiveEffects[ActiveEffects.IndexOf(existingActiveEffect)] = effect;
            }
            else
            {
                ActiveEffects.Add(effect);
            }

            return effect.Handle;
        }

        public void RemoveGameplayEffectsWithAssetTags(GameplayTagContainer tags)
        {
            foreach(var activeEffect in ActiveEffects.ToList())
            {
                if (activeEffect.Specification.EffectSo.assetTags.HasAny(tags))
                {
                    ActiveEffects.Remove(activeEffect);
                }
            }
        }

        public ActiveGameplayEffect GetActiveGameplayEffectByEffectSo(GameplayEffect effectSo)
        {
            foreach(var activeEffect in ActiveEffects)
            {
                if(activeEffect.Specification.EffectSo == effectSo)
                {
                    return activeEffect;
                }
            }

            return null;
        }
        
        public void OnTimeChanged(ConcreteEventArgs eventArgs)
        {
            var expiredEffects = new List<ActiveGameplayEffect>();
            
            foreach (var activeEffect in ActiveEffects)
            {
                activeEffect.TickDuration(eventArgs, _owner);

                if (activeEffect.RemainingDuration <= 0)
                {
                    expiredEffects.Add(activeEffect);
                }
            }

            foreach (var expiredEffect in expiredEffects)
            {
                ActiveEffects.Remove(expiredEffect);
            }
        }
        
        public IEnumerator GetEnumerator()
        {
            return ActiveEffects.GetEnumerator();
        }
    }
}
