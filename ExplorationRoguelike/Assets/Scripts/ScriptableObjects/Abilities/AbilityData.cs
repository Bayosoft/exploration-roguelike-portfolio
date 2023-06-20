using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(menuName = "Abilities")]
    public abstract class AbilityData : ScriptableObject
    {
        public string Name;
        public List<GameplayTag> Tags;
        public List<string> Description;

        /// <summary>
        /// Overridden by specific ability implementations.
        /// </summary>
        /// <param name="instigator"></param>
        /// <param name="targets"></param>
        /// <param name="activationType"></param>
        public abstract void Activate(Character instigator, IEnumerable<Character> targets);

        /// <summary>
        /// Overridden by specific ability implementations.
        /// </summary>
        /// <param name="instigator"></param>
        /// <param name="targets"></param>
        /// <param name="activationType"></param>
        public abstract void Activate(Character instigator, Character target);
    }
}
