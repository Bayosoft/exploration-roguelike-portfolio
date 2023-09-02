using System;
using System.Collections;
using System.Collections.Generic;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Combat.Events;
using UnityEngine;

namespace ExplorationRoguelike.GameplayEffects
{
    //  Holds data directly tied to the effect being applied, including ActiveGameplayEffectHandle, GameplayEffectSpec and its start or end time, etc.
    public class ActiveGameplayEffect
    {

        public ActiveGameplayEffect(GameplayEffectSpecification effectSpec) 
        {
            Handle = ActiveGameplayEffectHandle.GenerateNew();
            Specification = effectSpec;
            RemainingDuration = effectSpec.Duration;
            IsInhibited = false;
        }
        public event EventHandler DurationChanged;
        public ActiveGameplayEffectHandle Handle { get; private set; }
        public GameplayEffectSpecification Specification { get; private set; }

        private int _remainingDuration;
        public int RemainingDuration
        {
            get => _remainingDuration;
            private set
            {
                _remainingDuration = value;
                DurationChanged?.Invoke(this, EventArgs.Empty);
            } 
        }

        public bool IsInhibited { get; private set; }

        public void TickDuration(ConcreteEventArgs eventArgs, AbilitySystemComponent owner)
        {
            switch (Specification.EffectSo.durationType)
            {
                case GameplayDurationType.Turns:
                    var endTurnEventArgs = eventArgs.ValidateEventArgs<EndTurnEventArgs>();

                    if (endTurnEventArgs.Initiator.Owner == owner)
                    {
                        RemainingDuration -= 1;
                    }
                    break;
                case GameplayDurationType.Time:
                    
                    var timeTickEventArgs = eventArgs.ValidateEventArgs<TimeTickEventArgs>();
                    
                    // Reduce remaining duration using a global Time calculator (and decide on what the default time tracker keeps track of (probably minutes).
                    break;
            }
        }
    }
}
