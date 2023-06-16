using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{

    [CreateAssetMenu(fileName = "Damage Part", menuName = "ScriptableObjects/Abilities/Parts/Damage Part", order = 1)]
    public class DamagePartData : AbilityPartData
    {

        public int MinDamage, MaxDamage;

        public int GetDamage()
        {
            return Random.Range(MinDamage, MaxDamage);
        }
        public override void Activate(AbilityComponent instigator, IEnumerable<Character> targets)
        {
            foreach (Character c in targets)
            {
                var hc = c.GetComponent<HealthComponent>();
                hc.ReduceHealthBy(GetDamage());
            }
        }

        public override void Activate(AbilityComponent instigator, Character target)
        {
            target.GetComponent<HealthComponent>().ReduceHealthBy(GetDamage());
        }

/*        public override string ToString()
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
        }*/
    }
}
