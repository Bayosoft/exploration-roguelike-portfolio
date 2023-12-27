using System.Collections.Generic;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class GameState : MonoBehaviour
    {
        private CombatStateComponent _combatStateComponent;
        public GameObject player;
        public GameObject enemy;
        public GameObject gameOverlay;

        void Awake()
        {
            DontDestroyOnLoad(this);
            Instantiate(player);
            DontDestroyOnLoad(gameOverlay);

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        public void SetCombat(GameObject enemyPrefab)
        {
            enemy = enemyPrefab;
        }
        
        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "CombatScene")
            {
                _combatStateComponent = FindAnyObjectByType<CombatStateComponent>();
                InitializeCombat();
            }
        }
        private void InitializeCombat()
        {
            _combatStateComponent.Initialize(player, enemy);
        }


    }
}
