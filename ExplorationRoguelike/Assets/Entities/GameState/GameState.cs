using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class GameState : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemy;
        public GameObject gameOverlay;

        private Player _playerInstance;

        private PlayerStatusEffectDisplayer _playerStatusEffectDisplayer;
        private CombatStateComponent _combatStateComponent;

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

                _combatStateComponent.Initialize(_playerInstance, enemy);
            }
        }
    }
}
