using System;
using System.Collections.Generic;

namespace ExplorationRoguelike
{
    public class LootExplorationTile : ExplorationTile
    {
        private LootTable lootTable;
        internal void SetRandomLoot(List<LootTable> lootTables)
        {
            if (lootTables != null && lootTables.Count > 0)
            {
                lootTable = lootTables[UnityEngine.Random.Range(0, lootTables.Count)];
            }
        }

        public override void ExploreTile()
        {
            if (tileSelectTransition is LootTransition lootTransition)
            {
                lootTransition.Transition(lootTable, null);
            }        
        }
    }
}