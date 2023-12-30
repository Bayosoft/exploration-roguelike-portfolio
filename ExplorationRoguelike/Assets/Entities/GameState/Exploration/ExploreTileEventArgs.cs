using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class ExploreTileEventArgs : ConcreteEventArgs
    {
        public ExplorationTile Tile { get; private set; }

        public ExploreTileEventArgs(ExplorationTile tile)
        {
            Tile = tile;
        }
    }
}
