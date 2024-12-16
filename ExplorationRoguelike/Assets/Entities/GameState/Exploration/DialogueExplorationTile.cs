using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class DialogueExplorationTile : ExplorationTile
    {
        public Dialogue Dialogue { get; set; }


        public override void OnSpawn(MapContents mapContents)
        {
            Dialogue = mapContents.RestSiteDialogue;
        }

        public override void Selected()
        {
            base.Selected();
            DialogueTransition.Instance.Transition(Dialogue, LoadSceneMode.Additive);

            this.enabled = false;
        }
    }
}