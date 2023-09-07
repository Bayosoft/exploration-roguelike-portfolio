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

        [SerializeField] private Player player;
        [SerializeField] private CombatNpc enemy;
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
            enemy.CharacterData = weakEnemy;
            gameState.InitializeCombat(player, enemy);
            SceneManager.LoadScene("CombatScene");
        }
        
        public void OnAverageEnemyClick()
        {
            // set combat enemy 
            enemy.CharacterData = averageEnemy;
            SceneManager.LoadScene("CombatScene");
        }

        public void OnBossEnemyClick()
        {
            // set combat enemy 
            enemy.CharacterData = bossEnemy;
            SceneManager.LoadScene("CombatScene");
        }

    }
}
