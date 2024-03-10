using ExplorationRoguelike.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "CombatMultiChoiceOption", menuName = "Dialogue/Combat Option")]
    public class CombatMultiChoiceOption : DialogueOption
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
