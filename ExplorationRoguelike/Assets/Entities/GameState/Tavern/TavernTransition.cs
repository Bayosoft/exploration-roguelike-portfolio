using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class TavernTransition : TransitionBase
    {
        public static TavernTransition Instance { get; private set; }

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
        }

        public void Transition(LoadSceneMode loadSceneMode)
        {
            StartCoroutine(LoadScene(loadSceneMode));
        }

        // Switch to tavern
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != sceneName)
            {
                return;
            }
            var tavern = FindAnyObjectByType<TavernStateComponent>();

            tavern.Initialize();

        }
    }
}
