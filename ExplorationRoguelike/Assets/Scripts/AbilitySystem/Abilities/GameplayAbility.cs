using ExplorationRoguelike.GameplayTags;
using ExplorationRoguelike.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
        public string Name;
        public AbilityType AbilityType;
        public List<GameplayTag> Tags;

        [SerializeField]
        private DescriptionText _description;
        public DescriptionText Description => _description;

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
