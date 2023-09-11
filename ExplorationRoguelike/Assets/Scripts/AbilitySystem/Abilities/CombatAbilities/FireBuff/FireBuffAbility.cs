using System.Collections.Generic;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.FireBuff
{
    public class FireBuffAbility : GameplayAbility, IPlayableCard
    {
        public GameplayEffect gameplayEffect;
        
        [field: SerializeField]
        public int ManaCost { get; private set; }
        
        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            GameplayEffectSpecification spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);

            if(spec != null)
            {
                instigator.ApplyGameplayEffectSpecToSelf(spec);
            }
            // Ability Fired event.
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            GameplayEffectSpecification spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);
            instigator.ApplyGameplayEffectSpecToSelf(spec);
        }
    }
}
