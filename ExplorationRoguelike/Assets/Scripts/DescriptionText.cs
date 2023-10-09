using System;
using ExplorationRoguelike.AbilitySystem;
using UnityEngine;

namespace ExplorationRoguelike
{
    [Serializable]
    public class DescriptionText
    {
        [SerializeField] private string description;
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
