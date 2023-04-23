using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class CombatTurnComponent : MonoBehaviour
    {
        public ScriptableEvent TakeTurnEvent;
        // Start is called before the first frame update
        void Start()
        {
            TakeTurnEvent.TriggerEvent(new TakeTurnEventArgs(new Enemy(), new Enemy(), 5));
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
