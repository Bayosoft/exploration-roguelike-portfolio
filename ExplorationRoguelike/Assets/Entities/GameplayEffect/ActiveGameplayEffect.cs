using System;
using System.Collections;
using System.Collections.Generic;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Combat.Events;

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
            // Subscribe to time ticker if Specification.EffectSo.durationType == Time.
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
                case GameplayDurationType.Turns when eventArgs.TryValidateEventArgs<EndTurnEventArgs>(out var endTurnEventArgs):
                    if (endTurnEventArgs.Initiator.Owner == owner)
                    {
                        RemainingDuration -= 1;
                    }
                    break;
                case GameplayDurationType.Time when eventArgs.TryValidateEventArgs<TimeTickEventArgs>(out var timeTickEventArgs):
                    RemainingDuration -= timeTickEventArgs.AmountOfTimeHours;
                    break;
            }
        }

        public void ExpireOnCombatEnded()
        {
            if(Specification.EffectSo.durationType == GameplayDurationType.Turns)
            {
                RemainingDuration = 0;
            }
        }
    }
}
