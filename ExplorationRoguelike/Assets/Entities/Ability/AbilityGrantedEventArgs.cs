using ExplorationRoguelike.AbilitySystem.Abilities;
using System;

namespace ExplorationRoguelike
{
    public class AbilityGrantedEventArgs : EventArgs
    {
        public GameplayAbility GrantedAbility { get; private set; }
        public AbilityGrantedEventArgs(GameplayAbility ability)
        {
            GrantedAbility = ability;
        }
    }
}
