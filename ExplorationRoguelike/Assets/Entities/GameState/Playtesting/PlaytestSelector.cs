using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private GameObject enemyPrefab;

        public void Start()
        {
            player = FindAnyObjectByType<Player>();
        }

        public void DeckSelected()
        {
            deckButtons.SetActive(false);
            enemyButtons.SetActive(true);
        }
        
        public void OnWeakDeckClick()
        {
            player.CharacterData.Abilities.Clear();
            player.CharacterData.Abilities.AddRange(weakDeck.abilities);
            DeckSelected();
        }
        
        public void OnAverageDeckClick()
        {
            player.CharacterData.Abilities.Clear();
            player.CharacterData.Abilities.AddRange(averageDeck.abilities); 
            DeckSelected();
        }

        public void OnStrongDeckClick()
        {
            player.CharacterData.Abilities.Clear();
            player.CharacterData.Abilities.AddRange(strongDeck.abilities);
            DeckSelected();
        }

        public void OnWeakEnemyClick()
        {
            // set combat enemy 
            var enemy = enemyPrefab.GetComponent<CombatNpc>();

            enemy.CharacterData = weakEnemy;
            
            gameState.SetCombat(enemyPrefab);
            SceneManager.LoadScene("CombatScene");
        }
        
        public void OnAverageEnemyClick()
        {
            // set combat enemy 
            var enemy = enemyPrefab.GetComponent<CombatNpc>();
            enemy.CharacterData = averageEnemy;
            gameState.SetCombat(enemyPrefab);
            SceneManager.LoadScene("CombatScene");
        }

        public void OnBossEnemyClick()
        {
            // set combat enemy 
            
            var enemy = enemyPrefab.GetComponent<CombatNpc>();
            enemy.CharacterData = bossEnemy;
            gameState.SetCombat(enemyPrefab);
            SceneManager.LoadScene("CombatScene");
        }

    }
}
