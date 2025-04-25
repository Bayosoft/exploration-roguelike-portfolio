using System;
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
        private Material lineMaterial;

        [SerializeField]
        protected ScriptableEvent exploreTileEvent;

        public List<ExplorationTile> ConnectedTiles { get; private set; } = new List<ExplorationTile>();

        public bool IsFinal { get; private set; }

        public abstract void OnSpawn(MapContents mapContents);

        public void SetIsFinal(bool isFinal)
        {
            IsFinal = isFinal;
        }

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

            var mapLine = new GameObject().AddComponent<LineRenderer>();
            mapLine.textureMode = LineTextureMode.Tile;
            mapLine.materials = new Material[1] { lineMaterial };
            mapLine.transform.parent = this.transform;
            mapLine.textureScale = new Vector2(0.1f, 0.1f);
            mapLine.positionCount = 2;
            mapLine.startWidth = 5;

            float width = mapLine.startWidth;
            mapLine.material.mainTextureScale = new Vector2(1f / width, 1.0f);

            mapLine.SetPosition(mapLine.positionCount - 2, sp);
            mapLine.SetPosition(mapLine.positionCount - 1, ep);


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