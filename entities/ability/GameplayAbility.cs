using ExplorationRoguelike.GameplayTags;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

namespace ExplorationRoguelike.AbilitySystem.Abilities
{
    [Serializable]
    public enum AbilityType
    {
        Unspecified,
        Combat,
        Dialogue,
        GameplayEvent,
        GameplayAction
    }

    public abstract partial class GameplayAbility : Resource, IDescribable
    {
        [Export]
        public string Name;

        [Export]
        public AbilityType abilityType;

        [Export]
        public Array<GameplayTag> generalTags;

        [Export]
        private DescriptionText description;
        public DescriptionText Description => description;

        // OnAbilityActivated event 
        
        /// <summary>
        /// Overridden by specific ability implementations.
        /// </summary>
        /// <param name="instigator"></param>
        /// <param name="targets"></param>
        /// <param name="activationType"></param>
        public abstract void Activate(AbilitySystemComponent instigator, IEnumerable<AbilitySystemComponent> targets);

        /// <summary>
        /// Overridden by specific ability implementations.
        /// </summary>
        /// <param name="instigator"></param>
        /// <param name="targets"></param>
        /// <param name="activationType"></param>
        public abstract void Activate(AbilitySystemComponent instigator, AbilitySystemComponent target);
    }
}
