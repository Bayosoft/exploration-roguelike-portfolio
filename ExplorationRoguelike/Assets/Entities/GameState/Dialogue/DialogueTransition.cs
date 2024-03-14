using ExplorationRoguelike.Combat;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class DialogueTransition : TransitionBase
    {
        private Dialogue _dialogue;

        public static DialogueTransition Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Transition(Dialogue dialogue, LoadSceneMode loadSceneMode)
        {
            _dialogue = dialogue;

            SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }

        // Switch to combat
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != sceneName)
            {
                return;
            }
            var dialogueState = FindAnyObjectByType<DialogueStateComponent>();

            dialogueState.Initialize(_dialogue);

        }

        // contains dialogueState data and switches scene to dialogueState.
        // Also should keep track of whether it should switch while keeping previous scene.


        // Transition to Dialoguestate
    }
}
