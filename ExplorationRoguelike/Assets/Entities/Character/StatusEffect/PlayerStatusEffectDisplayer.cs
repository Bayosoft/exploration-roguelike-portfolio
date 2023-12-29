using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    public class PlayerStatusEffectDisplayer : StatusEffectDisplayerBase
    {
        public void Start()
        {
            DontDestroyOnLoad(this);
        }

        protected override void AddStatusEffect(ActiveGameplayEffect addedEffect)
        {

            var effectObject = Instantiate(statusEffectPrefab, gameObject.transform, true);
            effectObject.transform.localScale = Vector2.one;
            effectObject.transform.localPosition = Vector2.one;
            effectObject.transform.SetAsLastSibling();
            var effectComponent = effectObject.GetComponent<StatusEffect>();
            effectComponent.Initialize(addedEffect);

            StatusEffects.Add(effectObject);
        }
    }
}
