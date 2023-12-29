using ExplorationRoguelike.GameplayTags;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    public abstract class GameplayAbility : ScriptableObject, IDescribable
    {
        public new string name;
        
        public bool IsUnique;
        public AbilityType abilityType;
        
        [FormerlySerializedAs("tags")]
        public List<GameplayTag> generalTags;

        [SerializeField]
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
