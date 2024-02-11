using ExplorationRoguelike.Characters;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "CombatMultiChoiceOptionData", menuName = "Multiple Choice Options/Combat Option")]
    public class CombatMultiChoiceOptionData : MultiChoiceOptionData
    {
        [SerializeField]
        private CharacterData enemy;

        public override void Activate()
        {
            FindAnyObjectByType<GameState>().SetCombatAdditive(enemy);
        }
    }
}
