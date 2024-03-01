using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
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

        [SerializeField] private MultiChoiceOptionData multiChoiceData;

        [SerializeField] private GameObject deckButtons;
        [SerializeField] private GameObject enemyButtons;

        [SerializeField] private GameState gameState;
        [SerializeField] private CombatTransition combatTransitioner;

        private Player player;

        public void Start()
        {
            player = FindAnyObjectByType<Player>();

            if(SceneManager.GetActiveScene().name == "ExplorationSetupScene")
            {
                SceneManager.LoadScene("ExplorationScene");
            }
            if(SceneManager.GetActiveScene().name == "MultiChoiceSetupScene")
            {
                gameState.SetDialogue(multiChoiceData);
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
            combatTransitioner.Transition(weakEnemy, LoadSceneMode.Single);
        }
        
        public void OnAverageEnemyClick()
        {
            combatTransitioner.Transition(averageEnemy, LoadSceneMode.Single);
        }

        public void OnBossEnemyClick()
        {
            combatTransitioner.Transition(bossEnemy, LoadSceneMode.Single);
        }

    }
}
