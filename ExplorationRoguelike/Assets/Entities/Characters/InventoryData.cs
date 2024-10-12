using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Character/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        public ObservableCollection<Artifact> Artifacts { get; set; }

        public int Gold { get; private set; }

        public void AddGold(int amount) => Gold += amount;

    }
}