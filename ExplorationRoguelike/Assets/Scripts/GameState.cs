using System.Collections.Generic;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    public class GameState : MonoBehaviour
    {
        public GameObject combatStateObject;
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void InitializeCombat(Player player, CombatNpc enemy)
        {
            combatStateObject.GetComponent<CombatStateComponent>().player = player;
            combatStateObject.GetComponent<CombatStateComponent>().enemies = new List<CombatNpc>{enemy};
            
            var go = Instantiate(combatStateObject);
        }
    }
}
