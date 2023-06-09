using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/Ability", order = 1)]
    public class AbilitySO : ScriptableObject
    {
        public string Name;
        public List<string> Description;


        // TODO: Move to DamageAbilityData : AbilityData
        public int MinDamage, MaxDamage;

        public int DealDamage()
        {
            return Random.Range(MinDamage, MaxDamage);
        }

        // TODO: Remove this disguting piece of shit code and make it something actually usable.
        public override string ToString()
        {
            string abilityDescription = default;

            foreach (var part in Description)
            {
                if (part.Equals("@Min"))
                {
                    abilityDescription += $"{MinDamage}";
                }
                else if (part.Equals("@Max"))
                {
                    abilityDescription += $"{MaxDamage}";
                }
                else
                {
                    abilityDescription += part;
                }
            }

            return abilityDescription;
        }
    }
}
