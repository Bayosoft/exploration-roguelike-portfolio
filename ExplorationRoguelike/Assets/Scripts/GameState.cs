using ExplorationRoguelike.Combat;
using UnityEngine;

namespace ExplorationRoguelike.Scripts
{
    public class GameState : MonoBehaviour
    {
        public CombatStateComponent combatStateComponent;
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
