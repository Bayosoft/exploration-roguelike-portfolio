namespace ExplorationRoguelike
{
    // Holds info about the context the effect was activated from, such as instigator, ability CDO, etc
    public class GameplayEffectContext
    {
        public AbilitySystemComponent Instigator { get; private set; }
        public AbilityData Ability { get; private set; }

        public GameplayEffectContext(AbilitySystemComponent instigator, AbilityData ability)
        {
            Instigator = instigator;
            Ability = ability;
        }
    }
}
