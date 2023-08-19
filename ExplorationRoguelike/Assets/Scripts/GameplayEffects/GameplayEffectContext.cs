using ExplorationRoguelike.AbilitySystem;

namespace ExplorationRoguelike.GameplayEffects
{
    // Holds info about the context the effect was activated from, such as instigator, ability CDO, etc
    public class GameplayEffectContext
    {
        public AbilitySystemComponent Instigator { get; private set; }

        public GameplayEffectContext(AbilitySystemComponent instigator)
        {
            Instigator = instigator;
        }

        public bool IsValid()
        {
            return Instigator != null;
        }
    }
}
