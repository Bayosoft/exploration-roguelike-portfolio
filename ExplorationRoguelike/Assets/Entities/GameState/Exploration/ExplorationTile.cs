using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ExplorationRoguelike
{
    public abstract class ExplorationTile : MonoBehaviour
    {
        [SerializeField]
        protected ScriptableEvent exploreTileEvent;

        public List<ExplorationTile> ConnectedTiles { get; private set; } = new List<ExplorationTile>();

        public void ExploreTile()
        {
            exploreTileEvent.RaiseEvent(new ExploreTileEventArgs(this));
        }

        public void AddConnectedTile(ExplorationTile tile)
        {

            if (ConnectedTiles.Contains(tile)) 
            {
                return;
            }

            Vector3 sp = this.transform.position;
            Vector3 ep = tile.transform.position;

            LineRenderer lineRenderer;

            if (this.TryGetComponent<LineRenderer>(out var renderer))
            {
                lineRenderer = renderer;
                lineRenderer.positionCount += 2;
            }
            else
            {
                lineRenderer = this.AddComponent<LineRenderer>();
            }

            lineRenderer.SetPosition(lineRenderer.positionCount - 2, sp);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, ep);


            // hide tile
            tile.gameObject.SetActive(false);
            GetComponent<Button>().interactable = false;

            ConnectedTiles.Add(tile);
            Debug.Log($"Connected tile [{transform.position}] to [{tile.transform.position}]");
        }

        // OptionSelected when selected
        public virtual void Selected()
        {
            GetComponent<Image>().color = Color.green;

            ConnectedTiles.ForEach(tile => tile.InReach());
            GetComponent<Button>().interactable = false;
        }

        // Grey out and uninteractable when unselectable
        public void OutOfReach()
        {
            // reveal tile
            gameObject.SetActive(true);
            GetComponent<Image>().color = Color.grey;
            GetComponent<Button>().interactable = false;
        }

        // Color and interactable when selecatable
        public void InReach()
        {
            GetComponent<Image>().color = Color.white;
            ConnectedTiles.ForEach(tile => tile.OutOfReach());
            GetComponent<Button>().interactable = true;
        }

        // Set inactive when not in sight
        public void OutOfSight()
        {
            // hide tile
            gameObject.SetActive(false);
        }

    }
}