using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
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

        private CombatStateComponent _combatStateComponent;
        private ExplorationStateComponent _explorationStateComponent;
        private DialogueStateComponent _dialogueStateComponent;
        private MultiChoiceOptionData choiceData;

        void Awake()
        {
            DontDestroyOnLoad(this);

            _playerInstance = Instantiate(player).GetComponent<Player>();

            DontDestroyOnLoad(_playerInstance);
            DontDestroyOnLoad(gameOverlay);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start()
        {
            _playerStatusEffectDisplayer = FindAnyObjectByType<PlayerStatusEffectDisplayer>();
            _playerStatusEffectDisplayer.Initialize(_playerInstance);

            _playerGoldDisplayer = FindAnyObjectByType<PlayerGoldDisplayer>();
            _playerGoldDisplayer.Initialize(_playerInstance);
        }

        // Loads combat and keeps scene that loaded it.
        public void SetCombatAdditive(CharacterData enemyData)
        {
            this.enemyData = enemyData;
            SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
        }
        public void SetCombat(CharacterData enemyData)
        {
            this.enemyData = enemyData;
            SceneManager.LoadSceneAsync("CombatScene");
        }
        public void SetDialogue(MultiChoiceOptionData choiceData) /* Replace with DialogueData which holds all the dialogue, choices, and sprites */
        {
            this.choiceData = choiceData;
            SceneManager.LoadScene("DialogueScene");
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "CombatScene")
            {
                _combatStateComponent = FindAnyObjectByType<CombatStateComponent>();

                _combatStateComponent.Initialize(_playerInstance, enemyData);
            }
            if(scene.name == "ExplorationScene")
            {
                _explorationStateComponent = FindAnyObjectByType<ExplorationStateComponent>();

                // TODO: I guess irrelevant if im refactoring this whole thing anyway.
            }
            if (scene.name == "DialogueScene")
            {
                _dialogueStateComponent = FindAnyObjectByType<DialogueStateComponent>();

                _dialogueStateComponent.LoadDialogue(new List<MultiChoiceOptionData>() { choiceData });
                // TODO: I guess irrelevant if im refactoring this whole thing anyway.
            }
        }
    }
}
