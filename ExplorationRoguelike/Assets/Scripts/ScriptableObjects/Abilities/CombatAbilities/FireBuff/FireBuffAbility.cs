using Noesis;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ExplorationRoguelike
{

    [CreateAssetMenu(fileName = "FireBuff", menuName = "Abilities/Combat/FireBuff")]
    public class FireBuffAbility : CardAbility
    {
        public GameplayEffect GameplayEffect;

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            GameplayEffectSpecification spec = instigator.MakeOutgoingEffectSpec(GameplayEffect);

            if(spec != null)
            {
                instigator.ApplyGameplayEffectSpecToSelf(spec);
            }
            // Ability Fired event.
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            GameplayEffectSpecification spec = instigator.MakeOutgoingEffectSpec(GameplayEffect);
            instigator.ApplyGameplayEffectSpecToSelf(spec);
        }
    }
}
