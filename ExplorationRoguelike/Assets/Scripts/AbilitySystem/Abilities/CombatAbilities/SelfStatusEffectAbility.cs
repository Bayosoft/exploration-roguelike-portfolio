using System.Collections.Generic;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities
{
    
    [CreateAssetMenu(fileName = "Status Effect (self)", menuName = "Abilities/Combat/Status Effect (self)")]
    public class SelfStatusEffectAbility : GameplayAbility
    {
        public GameplayEffect gameplayEffect;
        
        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            var spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);

            if(spec != null)
            {
                instigator.ApplyGameplayEffectSpecToSelf(spec);
            }
            // Ability Fired event.
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            var spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);
            instigator.ApplyGameplayEffectSpecToSelf(spec);
        }
    }
}
