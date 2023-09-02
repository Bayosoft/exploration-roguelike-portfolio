using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayEffects;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Character
{
    public class StatusEffectViewModel : MonoBehaviour
    {
        [SerializeField] private GameObject statusEffectPrefab;

        [SerializeField] private AbilitySystemComponent characterAbilitySystem;

        public ObservableCollection<GameObject> StatusEffectViews { get; set; }

        public void Awake()
        {
            StatusEffectViews = new ObservableCollection<GameObject>();
        }

        public void Start()
        {
            characterAbilitySystem.ActiveGameplayEffects.ActiveEffects.CollectionChanged +=
                PrintStatusEffect;
        }

        public void PrintStatusEffect(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var effectView = Instantiate(statusEffectPrefab, gameObject.transform, true);
                effectView.transform.localScale = Vector2.one;
                effectView.transform.localPosition = Vector2.one;
                effectView.transform.SetAsLastSibling();
                var effectComponent = effectView.GetComponent<StatusEffect>();
                effectComponent.Initialize((ActiveGameplayEffect)e.NewItems[0]);

                StatusEffectViews.Add(effectView);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var removedEffect = (GameplayEffect)e.OldItems[0];

                foreach (var effectView in
                         StatusEffectViews.ToList()
                             .Where(effectView => effectView.GetComponent<GameplayEffect>() == removedEffect))
                {
                    StatusEffectViews.Remove(effectView);
                    Destroy(effectView);
                    return;
                }
            }
        }
    }
}