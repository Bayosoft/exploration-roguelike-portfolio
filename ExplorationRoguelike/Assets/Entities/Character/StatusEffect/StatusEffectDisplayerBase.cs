using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.Characters
{
    public abstract class StatusEffectDisplayerBase : MonoBehaviour
    {
        [SerializeField] protected GameObject statusEffectPrefab;

        protected AbilitySystemComponent _characterAbilitySystem;

        public ObservableCollection<GameObject> StatusEffects { get; set; }

        public void Awake()
        {
            StatusEffects = new ObservableCollection<GameObject>();
        }
        public void Initialize(Character character)
        {
            _characterAbilitySystem = character.AbilitySystemComponent;

            _characterAbilitySystem.ActiveGameplayEffects.ActiveEffects.CollectionChanged +=
            PrintStatusEffect;
        }

        public void PrintStatusEffect(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        AddStatusEffect((ActiveGameplayEffect)e.NewItems[0]);
                        break;
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        RemoveStatusEffectView((ActiveGameplayEffect)e.OldItems[0]);
                        break;
                    }
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    RemoveStatusEffectView((ActiveGameplayEffect)e.OldItems[0]);
                    AddStatusEffect((ActiveGameplayEffect)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void AddStatusEffect(ActiveGameplayEffect addedEffect);

        private void RemoveStatusEffectView(ActiveGameplayEffect removedEffect)
        {
            foreach (var effect in
                     StatusEffects.ToList()
                         .Where(effect => effect.GetComponent<StatusEffect>().GameplayEffect == removedEffect))
            {
                StatusEffects.Remove(effect);
                Destroy(effect);
                return;
            }

        }
    }
}