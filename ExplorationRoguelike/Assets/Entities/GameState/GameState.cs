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
        private CharacterData enemyData;
        public GameObject gameOverlay;

        private Player _playerInstance;

        private PlayerStatusEffectDisplayer _playerStatusEffectDisplayer;
        private PlayerGoldDisplayer _playerGoldDisplayer;

        private DialogueStateComponent _dialogueStateComponent;
        private MultiChoiceOptionData choiceData;

        public event EventHandler GameLoaded;

        [SerializeField]
        private CombatTransition combatTransitioner;

        void Awake()
        {
            DontDestroyOnLoad(this);

            _playerInstance = Instantiate(player).GetComponent<Player>();
            DontDestroyOnLoad(_playerInstance);
            DontDestroyOnLoad(gameOverlay);

            combatTransitioner.Initialize(_playerInstance);

            SceneManager.sceneLoaded += OnSceneLoaded;

            GameLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void Start()
        {
            _playerStatusEffectDisplayer = FindAnyObjectByType<PlayerStatusEffectDisplayer>();
            _playerStatusEffectDisplayer.Initialize(_playerInstance);

            _playerGoldDisplayer = FindAnyObjectByType<PlayerGoldDisplayer>();
            _playerGoldDisplayer.Initialize(_playerInstance);
        }

        // TODO: Remove and refactor into DialogueTransition
        public void SetDialogue(MultiChoiceOptionData choiceData) /* Replace with DialogueData which holds all the dialogue, choices, and sprites */
        {
            this.choiceData = choiceData;
            SceneManager.LoadScene("DialogueScene");
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "DialogueScene")
            {
                _dialogueStateComponent = FindAnyObjectByType<DialogueStateComponent>();

                _dialogueStateComponent.LoadDialogue(new List<MultiChoiceOptionData>() { choiceData });
                // TODO: I guess irrelevant if im refactoring this whole thing anyway.
            }
        }
    }
}
