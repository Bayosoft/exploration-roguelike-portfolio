using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class DialogueExplorationTile : ExplorationTile
    {
        public Dialogue Dialogue { get; set; }
        public override void ExploreTile()
        {
            exploreTileEvent.RaiseEvent(new ExploreTileEventArgs(this));
        }

        public override void Selected()
        {
            base.Selected();
            DialogueTransition.Instance.Transition(Dialogue, LoadSceneMode.Additive);

            this.enabled = false;
        }
    }
}