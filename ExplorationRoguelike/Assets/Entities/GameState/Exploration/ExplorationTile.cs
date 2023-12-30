using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class ExplorationTile : MonoBehaviour
    {
        [SerializeField]
        protected ScriptableEvent exploreTileEvent;

        public abstract void ExploreTile();
    }
}