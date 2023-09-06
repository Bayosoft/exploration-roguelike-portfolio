using System.Collections;
using System.Collections.Generic;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class PlaytestSelector : MonoBehaviour
    {
        [SerializeField] private PlayerCharacterData player;
        [SerializeField] private PlayTestAbilityCollection weakDeck;
        [SerializeField] private PlayTestAbilityCollection averageDeck;
        [SerializeField] private PlayTestAbilityCollection strongDeck;
        
        [SerializeField] private CharacterData weakEnemy;
        [SerializeField] private CharacterData averageEnemy;
        [SerializeField] private CharacterData bossEnemy;

        [SerializeField] private GameObject deckButtons;
        [SerializeField] private GameObject enemyButtons;


        public void DeckSelected()
        {
            deckButtons.SetActive(false);
            enemyButtons.SetActive(true);
        }
        
        public void OnWeakDeckClick()
        {
            player.Abilities.Clear();
            player.Abilities.AddRange(weakDeck.abilities);
            DeckSelected();
        }
        
        public void OnAverageDeckClick()
        {
            player.Abilities.Clear();
            player.Abilities.AddRange(averageDeck.abilities); 
            DeckSelected();
        }

        public void OnStrongDeckClick()
        {
            player.Abilities.Clear();
            player.Abilities.AddRange(strongDeck.abilities);
            DeckSelected();
        }

        public void OnWeakEnemyClick()
        {
            // set combat enemy 
            
            SceneManager.LoadScene("CombatScene");
        }
        
        public void OnAverageEnemyClick()
        {
            // set combat enemy 
            SceneManager.LoadScene("CombatScene");
        }

        public void OnBossEnemyClick()
        {
            // set combat enemy 
            SceneManager.LoadScene("CombatScene");
        }

    }
}
