using System;
using ExplorationRoguelike.AbilitySystem;
using Godot;

namespace ExplorationRoguelike
{
    public partial class DescriptionText : Resource
    {
        [Export] private string description;
        protected DescriptionText(IDescribable owner)
        {
            if(owner is IModifiable modifiable)
            {
                modifiable.OnModifiersCalculated += UpdateText;
            }
        }

        private void UpdateText(int modifiedValue)
        {
            throw new NotImplementedException();
        }

        // TODO: Add a way to build a description 
        public override string ToString()
        {
            return description;
        }
    }
}
