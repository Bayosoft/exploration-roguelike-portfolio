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
        void Awake()
        {
            DontDestroyOnLoad(this);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        public void SetCombatants(GameObject playerPrefab, GameObject enemyPrefab)
        {
            player = playerPrefab;
            enemy = enemyPrefab;
        }
        
        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "CombatScene")
            {
                _combatStateComponent = FindObjectOfType<CombatStateComponent>();
                InitializeCombat();
            }
        }
        private void InitializeCombat()
        {
            _combatStateComponent.Initialize(player, enemy);
        }


    }
}
