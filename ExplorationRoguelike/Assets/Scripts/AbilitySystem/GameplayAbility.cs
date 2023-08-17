using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ExplorationRoguelike
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

    public abstract class GameplayAbility : ScriptableObject
    {
        public string Name;
        public DescriptionText Description;
        public AbilityType AbilityType;
        public List<GameplayTag> Tags;

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

        public override string ToString()
        {
            StringBuilder sb = new();

            foreach(string line in Description)
            {
                sb.Append(line);
            }
            return sb.ToString();
        }
    }
}
