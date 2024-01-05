using ExplorationRoguelike.Characters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class PlayerGoldDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI goldText;
        public void Initialize(Character character)
        {
           /* character.CharacterData.ActiveGameplayEffects.ActiveEffects.CollectionChanged +=
            PrintStatusEffect;*/
        }
    }
}
