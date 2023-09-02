using System;
using ExplorationRoguelike.AbilitySystem;

namespace ExplorationRoguelike
{
    [Serializable]
    public class DescriptionText
    {

        public DescriptionText(IDescribable owner)
        {
            if(owner is IModifiable modifiable)
            {
                modifiable.OnModifiersCalculated += UpdateText;
            }
        }

        private void UpdateText(float modifiedValue)
        {
            throw new NotImplementedException();
        }

        // TODO: Add a way to build a description 
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
