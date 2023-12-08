using System.Collections.Generic;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.GameplayEffects;
using Godot;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.Frostbolt
{
    public partial class FrostboltAbility : InstantDamageAbility, IPlayableCard
    { 
        public GameplayEffect gameplayEffect;
        
        [Export]
        public TargetType TargetType { get; private set; }

        [Export]
        public int ManaCost { get; private set; }
        
        public override void Activate(AbilitySystemComponent instigator, IEnumerable<Character> targets)
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

        public override void Activate(AbilitySystemComponent instigator, Character target)
        {
            var spec = instigator.MakeOutgoingEffectSpec(gameplayEffect);

            // Temp damage testing
            base.Activate(instigator, target);
        }

    }
}
