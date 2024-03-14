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

        public override void Activate()
        {
            CombatTransition.Instance.Transition(enemy, LoadSceneMode.Additive);
        }
    }
}
