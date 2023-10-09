using System;

namespace ExplorationRoguelike.AbilitySystem.Abilities.CombatAbilities.Fireball
{
    [Serializable]
    public class FireballDescription : DescriptionText
    {
        public FireballDescription(IDescribable owner) : base(owner)
        {
        }
    }    
}