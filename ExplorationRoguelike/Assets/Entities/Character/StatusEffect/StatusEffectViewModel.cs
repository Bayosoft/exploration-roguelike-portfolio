using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Character
{
    // TODO: Rework.
    public class StatusEffectViewModel : MonoBehaviour
    {
        [SerializeField] private GameObject statusEffectPrefab;

        private AbilitySystemComponent _characterAbilitySystem;

        public ObservableCollection<GameObject> StatusEffectViews { get; set; }

        public void Awake()
        {
            StatusEffectViews = new ObservableCollection<GameObject>();
        }
        
        public void Start()
        {
            if (_characterAbilitySystem == null)
            {
                _characterAbilitySystem = GameObject.FindObjectOfType<Player>().AbilitySystemComponent;
            }
            _characterAbilitySystem.ActiveGameplayEffects.ActiveEffects.CollectionChanged +=
                PrintStatusEffect;
        }

        public void PrintStatusEffect(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    AddStatusEffectView((ActiveGameplayEffect)e.NewItems[0]);
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
                    AddStatusEffectView((ActiveGameplayEffect)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddStatusEffectView(ActiveGameplayEffect addedEffect)
        {
            var effectView = Instantiate(statusEffectPrefab, gameObject.transform, true);
            effectView.transform.localScale = Vector2.one;
            effectView.transform.localPosition = Vector2.one;
            effectView.transform.SetAsLastSibling();
            var effectComponent = effectView.GetComponent<StatusEffect>();
            effectComponent.Initialize(addedEffect);

            StatusEffectViews.Add(effectView);
        }

        private void RemoveStatusEffectView(ActiveGameplayEffect removedEffect)
        {
            foreach (var effectView in
                     StatusEffectViews.ToList()
                         .Where(effectView => effectView.GetComponent<StatusEffect>().GameplayEffect == removedEffect))
            {
                StatusEffectViews.Remove(effectView);
                Destroy(effectView);
                return;
            }

        }
    }
}