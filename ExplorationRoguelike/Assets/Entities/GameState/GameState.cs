using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class GameState : MonoBehaviour
    {
        public GameObject player;
        public GameObject gameOverlay;

        private Player _playerInstance;

        private PlayerStatusEffectDisplayer _playerStatusEffectDisplayer;
        private PlayerGoldDisplayer _playerGoldDisplayer;

        public event EventHandler GameLoaded;


        void Awake()
        {
            DontDestroyOnLoad(this);

            _playerInstance = Instantiate(player).GetComponent<Player>();
            DontDestroyOnLoad(_playerInstance);
            DontDestroyOnLoad(gameOverlay);

            CombatTransition.Instance.Initialize(_playerInstance);

            GameLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void Start()
        {
            _playerStatusEffectDisplayer = FindAnyObjectByType<PlayerStatusEffectDisplayer>();
            _playerStatusEffectDisplayer.Initialize(_playerInstance);

            _playerGoldDisplayer = FindAnyObjectByType<PlayerGoldDisplayer>();
            _playerGoldDisplayer.Initialize(_playerInstance);
        }

    
    }
}
