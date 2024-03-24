using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public abstract class TransitionBase : MonoBehaviour
    {
        // Base for transitions to different states
        // Should be used for anything that transitions to another state like exploration tiles, dialogue options, winning combat, and tavern stuff.

        [SerializeField]
        protected string sceneName;

        protected IEnumerator LoadScene(LoadSceneMode mode)
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, mode);
            yield return load;
        }

        protected IEnumerator UnloadScene()
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(sceneName);
            yield return unload;
        }
    }
}
