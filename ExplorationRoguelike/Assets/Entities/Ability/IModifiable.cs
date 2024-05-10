namespace ExplorationRoguelike.AbilitySystem
{
    public delegate void OnModifiersCalculated(int modifiedValue);

    public interface IModifiable
    {
        public event OnModifiersCalculated OnModifiersCalculated;
        public float CalculateModifiers(AbilitySystemComponent source, AbilitySystemComponent target);
    }
}
