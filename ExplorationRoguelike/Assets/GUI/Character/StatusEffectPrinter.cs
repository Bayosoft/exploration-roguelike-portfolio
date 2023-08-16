using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "Status Effect Printer", menuName = "Utility/Status Effect Printer")]
    public class StatusEffectPrinter : ScriptableObject
    {
        public GameObject StatusEffectPrefab;

        public StatusEffect PrintStatusEffect(GameplayEffect effect)
        {
            GameObject effectView = Instantiate(StatusEffectPrefab);

            StatusEffect effectComponent = effectView.GetComponent<StatusEffect>();
            effectComponent.Initialize(effect);

            return effectComponent;
        }
    }
}
