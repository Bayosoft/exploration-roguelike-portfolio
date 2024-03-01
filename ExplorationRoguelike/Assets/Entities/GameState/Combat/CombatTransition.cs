using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "CombatTransition", menuName = "Transitions/Combat Transition")]
    public class CombatTransition : TransitionBase
    {
        private CharacterData _enemyData;
        private Player _player;

        public override void Awake()
        {
            Debug.Log("Combat transition laoded");

            _player = FindAnyObjectByType<Player>();

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
            var combat = FindAnyObjectByType<CombatStateComponent>();

            combat.Initialize(_player, _enemyData);
        }
    }
}
