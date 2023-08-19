using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Scripts
{
    public interface IDescribable 
    {
        public DescriptionText Description { get; }
    }
}
