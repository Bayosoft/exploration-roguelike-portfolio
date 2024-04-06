using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.StatusEffect
{
    public class PlayerStatusEffectDisplayer : StatusEffectDisplayerBase
    {
        public void Start()
        {
            DontDestroyOnLoad(transform.parent);
        }

        protected override void AddStatusEffect(ActiveGameplayEffect addedEffect, GameObject freeSlot)
        {
            var effectObject = Instantiate(statusEffectPrefab, freeSlot.transform, true);
            effectObject.transform.localScale = Vector2.one;
            effectObject.transform.localPosition = Vector2.one;
            effectObject.transform.SetAsLastSibling();
            var effectComponent = effectObject.GetComponent<StatusEffect>();
            effectComponent.Initialize(addedEffect);

            // StatusEffects.Add(effectObject);
        }
    }
}
