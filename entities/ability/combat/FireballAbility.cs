using System.Collections.Generic;
using ExplorationRoguelike.GameplayEffects;
using ExplorationRoguelike.GameplayTags;
using Godot;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.Fireball
{
    [GlobalClass]
    public partial class FireballAbility : InstantDamageAbility, IPlayableCard
    {
        [Export]
        public TargetType TargetType { get; private set; }
        
        // Added in designer
        [Export]
        private GameplayTagContainer ignitedTagContainer = new();

        public GameplayEffect gameplayEffect;
        
        [Export]
        public int ManaCost { get; private set; }
        
        public override void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets)
        {
            var spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);

//             if(spec != null)
//             {
//                 foreach (AbilitySystemComponent target in targets)
//                 {
//                     if (target.HasAll(_ignitedTagContainer))
//                     {
//                         spec.Duration += 5;
//                     }
//                     instigator.ApplyGameplayEffectSpecToSelf(spec);
//                 }
//             }

            // Temp damage testing
            base.Activate(instigator, targets);
            // Ability Fired event.
        }

        public override void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            var spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);
            // Temp damage testing
            base.Activate(instigator, target);
        }
    }
}
