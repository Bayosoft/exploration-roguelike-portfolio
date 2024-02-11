using ExplorationRoguelike.Characters;
using System.Collections.Generic;

namespace ExplorationRoguelike
{
    public class CombatExplorationTile : ExplorationTile
    {
        public CharacterData EnemyData { get; set; }

        public override void ExploreTile()
        {
            exploreTileEvent.RaiseEvent(new ExploreTileEventArgs(this));
        }

        public override void Selected()
        {
            base.Selected();

            FindFirstObjectByType<GameState>().SetCombatAdditive(EnemyData);
            gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}
