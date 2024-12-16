using ExplorationRoguelike.Characters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{

    public class RandomEventExplorationTile : ExplorationTile
    { 

        private ScriptableObject randomEvent;

        public override void OnSpawn(MapContents mapContents)
        {
            SetRandomEvent(mapContents);
        }

        public void SetRandomEvent(MapContents mapContents)
        {
            List<ScriptableObject> eventPool = new();

            if (mapContents.EventEnemies != null && mapContents.EventEnemies.Count > 0)
            {
                eventPool.AddRange(mapContents.EventEnemies);
            }
            if(mapContents.EventLoot != null && mapContents.EventLoot.Count > 0)
            {
                eventPool.AddRange(mapContents.EventLoot);
            }
            // Dialogue

            randomEvent = eventPool[Random.Range(0, eventPool.Count)];
        }

        public override void Selected()
        {
            base.Selected();

            if (randomEvent != null)
            {
                if (randomEvent is LootTable lootTable)
                {
                    LootTransition.Instance.Transition(lootTable, null);
                }
                if (randomEvent is CharacterData enemy)
                {
                    CombatTransition.Instance.Transition(enemy, LoadSceneMode.Additive);
                }
                // Dialogue
            }

            this.enabled = false;
        }
    }    
}