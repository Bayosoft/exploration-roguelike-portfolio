using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterSO _characterData;
        public CharacterSO CharacterData { get { return _characterData; } }
    }
}
