using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class TavernReturnButton : MonoBehaviour
    {
        public void OnReturnToTavernClicked()
        {
            TavernTransition.Instance.Transition(LoadSceneMode.Single);
        }
    }
}
