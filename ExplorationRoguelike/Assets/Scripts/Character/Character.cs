using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterData _characterData;
        public CharacterData CharacterData { get { return _characterData; } }
    }
}
