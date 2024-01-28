using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ExplorationRoguelike
{
    public abstract class ExplorationTile : MonoBehaviour
    {
        [SerializeField]
        protected ScriptableEvent exploreTileEvent;

        public List<ExplorationTile> ConnectedTiles { get; private set; } = new List<ExplorationTile>();

        private bool explored;

        public abstract void ExploreTile();

        public void OnExplored()
        {
            explored = true;
            // tell next tiles to be selectable
            // tell tiles after next tile to be visible
        }

        public void AddConnectedTile(ExplorationTile tile)
        {
            ConnectedTiles.Add(tile);
            Debug.Log($"Connected tile [{transform.position}] to [{tile.transform.position}]");
        }

        // Selected when selected
        public void Selected()
        {
            GetComponent<Image>().color = Color.green;

            ConnectedTiles.ForEach(tile => tile.InReach());
            GetComponent<Button>().interactable = false;

            if (this is CombatExplorationTile combatTile)
            {
                GetComponent<GameState>().SetCombat(combatTile.EnemyData.First());
                gameObject.SetActive(false);
                this.enabled = false;
            }
        }

        // Grey out when unselectable
        public void OutOfReach()
        {
            // reveal tile
            GetComponent<Image>().color = Color.grey;
            GetComponent<Button>().interactable = false;
        }

        // Grey out when unselectable
        public void InReach()
        {
            GetComponent<Image>().color = Color.white;
            ConnectedTiles.ForEach(tile => tile.OutOfReach());
            GetComponent<Button>().interactable = true;
        }

        // Hide when 2 or more away
    }
}