using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class StartRunButton : MonoBehaviour
    {
        public void StartRun()
        {
            SceneManager.LoadScene("ExplorationScene");
        }
    }
}
