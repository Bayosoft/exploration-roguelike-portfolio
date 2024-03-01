using ExplorationRoguelike.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "CombatMultiChoiceOptionData", menuName = "Multiple Choice Options/Combat Option")]
    public class CombatMultiChoiceOptionData : MultiChoiceOptionData
    {
        [SerializeField]
        private CharacterData enemy;

        [SerializeField]
        private CombatTransition combatTransition;

        public override void Activate()
        {
            combatTransition.Transition(enemy, LoadSceneMode.Additive);
        }
    }
}
