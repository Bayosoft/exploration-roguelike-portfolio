using Noesis;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{

    [CreateAssetMenu(fileName = "Fireball", menuName = "Abilities/Combat/Fireball")]
    public class FireballAbility : GameplayAbility
    {
        // Added in designer
        [SerializeField]
        private GameplayTagContainer _ignitedTagContainer;

        public GameplayEffect GameplayEffect;

        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            GameplayEffectSpecification spec = instigator.MakeOutgoingEffectSpec(GameplayEffect);

            foreach(AbilitySystemComponent target in targets)
            {
                if (target.HasAll(_ignitedTagContainer))
                {
                    spec.Duration += 5;
                }
                instigator.ApplyGameplayEffectSpecToTarget(spec, target);
            }
            // Ability Fired event.
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
         //   AbilityExtensions.DamageSingleTarget(instigator, target, AbilityExtensions.GetRandomDamage(MinDamage, MaxDamage));
        }
    }
}
