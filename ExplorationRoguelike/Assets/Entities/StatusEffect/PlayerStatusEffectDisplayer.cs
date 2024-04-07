using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayEffects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike.StatusEffect
{
    public class PlayerStatusEffectDisplayer : StatusEffectDisplayerBase
    {
        public void Start()
        {
            DontDestroyOnLoad(transform.parent);
        }

        protected override void AddStatusEffect(ActiveGameplayEffect addedEffect)
        {
            var freeSlot = activeStatusesBySlot.FirstOrDefault(slot => slot.Value == null);
            if (freeSlot.Equals(default(KeyValuePair<GameObject, GameObject>)))
            {
                // Dont visually add the status effects (TODO: Make it appear when a slot becomes available)
                return;
            }
            var effectObject = Instantiate(statusEffectPrefab, freeSlot.Key.transform, true);
            effectObject.transform.localScale = Vector2.one;
            effectObject.transform.localPosition = Vector2.one;
            effectObject.transform.SetAsLastSibling();
            var effectComponent = effectObject.GetComponent<StatusEffect>();
            effectComponent.Initialize(addedEffect);

            activeStatusesBySlot[freeSlot.Key] = effectObject;
            // StatusEffects.Add(effectObject);
        }
    }
}
