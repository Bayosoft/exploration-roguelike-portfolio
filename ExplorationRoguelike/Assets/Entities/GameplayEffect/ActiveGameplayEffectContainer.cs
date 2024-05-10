using ExplorationRoguelike.AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ExplorationRoguelike.Combat.Events;
using ExplorationRoguelike.GameplayTags;
using UnityEngine;
using System;
using System.Diagnostics.Tracing;
using ExplorationRoguelike.GameState;

namespace ExplorationRoguelike.GameplayEffects
{
    [Serializable]
    //  A container with accessors specific to active gameplay effects, such as telling it to remove effects by application of a given effect
    public class ActiveGameplayEffectContainer : IEnumerable
    {
        public ObservableCollection<ActiveGameplayEffect> ActiveEffects;
        private readonly AbilitySystemComponent _owner;

        public ActiveGameplayEffectContainer(AbilitySystemComponent owner)
        {
            ActiveEffects = new ObservableCollection<ActiveGameplayEffect>();
            _owner = owner;
            TimeHandler.TimeChanged += OnTimeChanged;
            TimeHandler.GameplayDurationTypeChanged += OnGameplayDurationTypeChanged;
        }

        public ActiveGameplayEffectHandle ApplyGameplayEffectSpec(GameplayEffectSpecification effectSpec)
        {
            var effect = new ActiveGameplayEffect(effectSpec);
            var existingActiveEffect = GetActiveGameplayEffectByEffectSo(effectSpec.EffectSo);

            if (existingActiveEffect != null) // Replace effect. (Probably need to add logic for stackable effects)
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
            foreach (var activeEffect in ActiveEffects.ToList())
            {
                if (activeEffect.Specification.EffectSo.assetTags.HasAny(tags))
                {
                    ActiveEffects.Remove(activeEffect);
                }
            }
        }

        public ActiveGameplayEffect GetActiveGameplayEffectByEffectSo(GameplayEffect effectSo)
        {
            foreach (var activeEffect in ActiveEffects)
            {
                if (activeEffect.Specification.EffectSo == effectSo)
                {
                    return activeEffect;
                }
            }

            return null;
        }

        public void OnTimeChanged(object sender, ConcreteEventArgs eventArgs)
        {
            foreach (var activeEffect in ActiveEffects.ToList())
            {
                if(activeEffect == null)
                {
                    ActiveEffects.Remove(activeEffect);
                }

                activeEffect.TickDuration(eventArgs, _owner);

                if (activeEffect.RemainingDuration <= 0)
                {
                    ActiveEffects.Remove(activeEffect);
                }
            }
        }

        public void OnGameplayDurationTypeChanged(object sender, ConcreteEventArgs eventArgs)
        {
            if (eventArgs.ValidateEventArgs<GameplayDurationTypeChangedEventArgs>().NewDurationType != GameplayDurationType.Turns)
            {
                foreach (var activeEffect in ActiveEffects.Where(
                ae => ae.Specification.EffectSo.durationType == GameplayDurationType.Turns).ToList())
                {                  
                    ActiveEffects.Remove(activeEffect);
                }
            }
            
        }

        public IEnumerator GetEnumerator()
        {
            return ActiveEffects.GetEnumerator();
        }
    }
}
