using JetBrains.Annotations;
using UnityEngine;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    public class PlayerCharacterData : CharacterData
    {
        [SerializeField]
        private PlayerHealth playerHealth;
        public PlayerHealth PlayerHealth => playerHealth;
    }
}
