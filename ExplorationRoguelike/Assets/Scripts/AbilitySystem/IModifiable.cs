namespace ExplorationRoguelike.AbilitySystem
{
    public delegate void OnModifiersCalculated(float result);

    public interface IModifiable
    {

        public event OnModifiersCalculated OnModifiersCalculated;
        public float CalculateModifiers(AbilitySystemComponent source);
    }
}
