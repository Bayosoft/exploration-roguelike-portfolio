using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using ExplorationRoguelike.GameplayEffects;
using ExplorationRoguelike.StatusEffect;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class GameStateHandler : MonoBehaviour
    {
        public GameObject player;
        public GameObject gameOverlay;

        private Player _playerInstance;

        private PlayerStatusEffectDisplayer _playerStatusEffectDisplayer;
        private PlayerGoldDisplayer _playerGoldDisplayer;

        public event EventHandler GameLoaded;

        [SerializeField] private GameplayEffect exhaustion;

        void Awake()
        {
            DontDestroyOnLoad(this);

            _playerInstance = Instantiate(player).GetComponent<Player>();
            DontDestroyOnLoad(_playerInstance);
            DontDestroyOnLoad(gameOverlay);

            GameLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void Start()
        {
            CombatTransition.Instance.Initialize(_playerInstance);

            _playerStatusEffectDisplayer = FindAnyObjectByType<PlayerStatusEffectDisplayer>();
            _playerStatusEffectDisplayer.Initialize(_playerInstance);

            _playerGoldDisplayer = FindAnyObjectByType<PlayerGoldDisplayer>();
            _playerGoldDisplayer.Initialize(_playerInstance);

            var spec = _playerInstance.AbilitySystemComponent.MakeOutgoingEffectSpec(exhaustion);
            _playerInstance.AbilitySystemComponent.ApplyGameplayEffectSpecToSelf(spec);
        }

    
    }
}
