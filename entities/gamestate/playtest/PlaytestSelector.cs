using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using Godot;

namespace ExplorationRoguelike
{
    public partial class PlaytestSelector : Node
    {
        [Export] private PlayTestAbilityCollection weakDeck;
        [Export] private PlayTestAbilityCollection averageDeck;
        [Export] private PlayTestAbilityCollection strongDeck;
        
        [Export] private CharacterResource weakEnemy;
        [Export] private CharacterResource averageEnemy;
        [Export] private CharacterResource bossEnemy;

        [Export] private Node2D deckButtons;
        [Export] private Node2D enemyButtons;

        [Export] private GameState gameState;

        [Export] private Resource playerResource;
        [Export] private Resource enemyResource;
        public void DeckSelected()
        {
            deckButtons.Hide();
            enemyButtons.Show();
        }

        // TODO: Get Player
        public void OnWeakDeckClick()
        {
           /* var player = playerResource.GetComponent<Player>();
            player.CharacterData.Abilities.Clear();
            player.CharacterData.Abilities.AddRange(weakDeck.abilities);*/
            DeckSelected();
        }
        
        public void OnAverageDeckClick()
        {
           /* var player = playerResource.GetComponent<Player>();
            player.CharacterData.Abilities.Clear();
            player.CharacterData.Abilities.AddRange(averageDeck.abilities); */
            DeckSelected();
        }

        public void OnStrongDeckClick()
        {
           /* var player = playerResource.GetComponent<Player>();
            player.CharacterData.Abilities.Clear();
            player.CharacterData.Abilities.AddRange(strongDeck.abilities);*/
            DeckSelected();
        }

        // TODO: Get Enemy and load combat
        public void OnWeakEnemyClick()
        {
          /*  // set combat enemy 
            var enemy = enemyResource.GetComponent<CombatNpc>();

            enemy.CharacterData = weakEnemy;
            
            gameState.SetCombatants(playerResource, enemyResource);
            SceneManager.LoadScene("CombatScene");*/
        }
        
        public void OnAverageEnemyClick()
        {
            // set combat enemy 
          /*  var enemy = enemyResource.GetComponent<CombatNpc>();
            enemy.CharacterData = averageEnemy;
            gameState.SetCombatants(playerResource, enemyResource);
            SceneManager.LoadScene("CombatScene");*/
        }

        public void OnBossEnemyClick()
        {
            // set combat enemy 
            
         /*   var enemy = enemyResource.GetComponent<CombatNpc>();
            enemy.CharacterData = bossEnemy;
            gameState.SetCombatants(playerResource, enemyResource);
            SceneManager.LoadScene("CombatScene");*/
        }

    }
}
