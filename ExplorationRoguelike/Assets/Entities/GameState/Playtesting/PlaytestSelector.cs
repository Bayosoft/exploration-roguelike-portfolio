using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class PlaytestSelector : MonoBehaviour
    {
        [SerializeField] private PlayTestAbilityCollection weakDeck;
        [SerializeField] private PlayTestAbilityCollection averageDeck;
        [SerializeField] private PlayTestAbilityCollection strongDeck;
        
        [SerializeField] private CharacterData weakEnemy;
        [SerializeField] private CharacterData averageEnemy;
        [SerializeField] private CharacterData bossEnemy;

        [SerializeField] private GameObject deckButtons;
        [SerializeField] private GameObject enemyButtons;

        [SerializeField] private GameState gameState;

        private Player player;

        public void Start()
        {
            player = FindAnyObjectByType<Player>();

            if(SceneManager.GetActiveScene().name == "ExplorationSetupScene")
            {
                SceneManager.LoadScene("ExplorationScene");
            }
        }

        public void DeckSelected()
        {
            deckButtons.SetActive(false);
            enemyButtons.SetActive(true);
        }
        
        public void OnWeakDeckClick()
        {
            player.CharacterData.Abilities.Clear();

            foreach (GameplayAbility ability in weakDeck.abilities)
            {
                player.CharacterData.GrantAbility(ability);
            }

            DeckSelected();
        }
        
        public void OnAverageDeckClick()
        {
            player.CharacterData.Abilities.Clear();
            foreach (GameplayAbility ability in averageDeck.abilities)
            {
                player.CharacterData.GrantAbility(ability);
            }
            DeckSelected();
        }

        public void OnStrongDeckClick()
        {
            player.CharacterData.Abilities.Clear();
            foreach (GameplayAbility ability in strongDeck.abilities)
            {
                player.CharacterData.GrantAbility(ability);
            }
            DeckSelected();
        }

        public void OnWeakEnemyClick()
        {            
            gameState.SetCombat(weakEnemy);
            SceneManager.LoadScene("CombatScene");
        }
        
        public void OnAverageEnemyClick()
        {
            gameState.SetCombat(averageEnemy);
            SceneManager.LoadScene("CombatScene");
        }

        public void OnBossEnemyClick()
        {
            gameState.SetCombat(bossEnemy);
            SceneManager.LoadScene("CombatScene");
        }

    }
}
