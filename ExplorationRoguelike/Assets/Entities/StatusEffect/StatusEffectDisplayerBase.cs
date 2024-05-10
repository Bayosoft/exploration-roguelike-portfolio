using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.StatusEffect
{
    public abstract class StatusEffectDisplayerBase : MonoBehaviour
    {
        [SerializeField] protected GameObject statusEffectPrefab;

        [SerializeField] protected List<GameObject> statusSlots;

        protected Dictionary<GameObject, GameObject> activeStatusesBySlot;
        public ObservableCollection<GameObject> StatusEffects { get; set; }

        public void Awake()
        {
            StatusEffects = new ObservableCollection<GameObject>();
            activeStatusesBySlot = new Dictionary<GameObject, GameObject>();
            statusSlots.ForEach(slot => activeStatusesBySlot.Add(slot, null));
        }
        public void Initialize(Character character)
        {
            character.CharacterData.ActiveGameplayEffects.ActiveEffects.CollectionChanged +=
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
                        RemoveStatusEffect((ActiveGameplayEffect)e.OldItems[0]);
                        break;
                    }
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    RemoveStatusEffect((ActiveGameplayEffect)e.OldItems[0]);
                    AddStatusEffect((ActiveGameplayEffect)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void AddStatusEffect(ActiveGameplayEffect addedEffect);

        private void RemoveStatusEffect(ActiveGameplayEffect removedEffect)
        {
            foreach (var effectToRemove in
                     activeStatusesBySlot
                         .Where(statusBySlot => statusBySlot.Value != null && statusBySlot.Value.GetComponent<StatusEffect>().GameplayEffect == removedEffect))
            {
                activeStatusesBySlot[effectToRemove.Key] = null;
                // StatusEffects.Remove(effectToRemove);
                Destroy(effectToRemove.Value);
                return;
            }

        }
    }
}