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

        public abstract void ExploreTile();

        public void AddConnectedTile(ExplorationTile tile)
        {
            // hide tile
            tile.gameObject.SetActive(false);

            ConnectedTiles.Add(tile);
            Debug.Log($"Connected tile [{transform.position}] to [{tile.transform.position}]");
        }

        // Selected when selected
        public virtual void Selected()
        {
            GetComponent<Image>().color = Color.green;

            ConnectedTiles.ForEach(tile => tile.InReach());
            GetComponent<Button>().interactable = false;
        }

        // Grey out when unselectable
        public void OutOfReach()
        {
            // reveal tile
            gameObject.SetActive(true);
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
    }
}