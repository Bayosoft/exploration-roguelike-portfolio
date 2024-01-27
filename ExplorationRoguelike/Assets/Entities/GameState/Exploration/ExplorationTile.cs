using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }

        public void AddConnectedTile(ExplorationTile tile)
        {
            ConnectedTiles.Add(tile);
            Debug.Log($"Connected tile [{transform.position}] to [{tile.transform.position}]");
        }
    }
}