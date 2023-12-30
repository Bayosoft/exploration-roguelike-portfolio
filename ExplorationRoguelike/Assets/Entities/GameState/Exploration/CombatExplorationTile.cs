using ExplorationRoguelike.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class CombatExplorationTile : ExplorationTile
    {
        public List<CharacterData> EnemyData { get; set; }

        public override void ExploreTile()
        {
            exploreTileEvent.RaiseEvent(new ExploreTileEventArgs(this));
        }
    }
}
