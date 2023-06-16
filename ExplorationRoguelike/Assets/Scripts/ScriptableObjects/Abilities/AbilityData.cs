using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/Ability", order = 1)]
    public class AbilityData : ScriptableObject
    {
        public string Name;
        public List<string> Description;
        public List<AbilityPartData> AbilityParts;

        /// <summary>
        /// Overridden by specific ability types
        /// </summary>
        /// <param name="instigator"></param>
        /// <param name="targets"></param>
        /// <param name="activationType"></param>
        public void Activate(AbilityComponent instigator, IEnumerable<Character> targets)
        {
            foreach (AbilityPartData part in AbilityParts)
            {
                part.Activate(instigator, targets);
            }
        }

        public void Activate(AbilityComponent instigator, Character target)
        {
            // TODO: What to do with different effect orders?
            foreach (AbilityPartData part in AbilityParts)
            {
                part.Activate(instigator, target);
            }
        }
     
    }
}
