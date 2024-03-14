using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class CombatTransition : TransitionBase
    {
        private CharacterData _enemyData;
        private Player _player;
        public static CombatTransition Instance { get; private set; }

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

        internal void Initialize(Player player)
        {
            _player = player;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Transition(CharacterData enemyData, LoadSceneMode loadSceneMode)
        {
            Debug.Log("Transitioned");
            _enemyData = enemyData;

            SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }

        // Switch to combat
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != sceneName)
            {
                return;
            }
            var combat = FindAnyObjectByType<CombatStateComponent>();

            combat.Initialize(_player, _enemyData);

        }
    }
}
