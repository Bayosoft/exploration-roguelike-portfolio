using ExplorationRoguelike.Combat;
using System;
using Unity.VisualScripting;
using UnityEngine.Events;
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

            StartCoroutine(LoadScene(loadSceneMode));
        }

        public void OnUnloadDialogue(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(UnloadScene());

            SceneManager.sceneLoaded -= OnUnloadDialogue;
        }

        public void OnUnloadDialogue()
        {
            StartCoroutine(UnloadScene());

            SceneManager.sceneLoaded -= OnUnloadDialogue;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != sceneName)
            {
                return;
            }
            var dialogueState = FindAnyObjectByType<DialogueStateComponent>();

            dialogueState.Initialize(_dialogue);

        }
    }
}
