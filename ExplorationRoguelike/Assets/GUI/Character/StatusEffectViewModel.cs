using ExplorationRoguelike.GUI.Card;
using ExplortationRoguelike.GUI;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class StatusEffectViewModel : ViewModel
    {
        [SerializeField]
        private GameObject _statusEffectPrefab;

        [SerializeField]
        private AbilitySystemComponent _characterAbilitySystem;

        public ObservableCollection<GameObject> StatusEffectViews { get; set; }

        public void Awake()
        {
            StatusEffectViews = new ObservableCollection<GameObject>();
        }

        public void Start()
        {
            _characterAbilitySystem.ActiveGameplayEffects.ActiveEffects.CollectionChanged += new NotifyCollectionChangedEventHandler(PrintStatusEffect);
        }
        public void PrintStatusEffect(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                GameObject effectView = Instantiate(_statusEffectPrefab);
                effectView.transform.parent = gameObject.transform;
                effectView.transform.localScale = Vector2.one;
                effectView.transform.localPosition = Vector2.one;
                effectView.transform.SetAsLastSibling();
                StatusEffect effectComponent = effectView.GetComponent<StatusEffect>();
                effectComponent.Initialize(((ActiveGameplayEffect)e.NewItems[0]).Specification.EffectSO);

                StatusEffectViews.Add(effectView);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                GameplayEffect removedEffect = (GameplayEffect)e.OldItems[0];

                foreach (GameObject effectView in StatusEffectViews.ToList())
                {
                    if (effectView.GetComponent<GameplayEffect>() == removedEffect)
                    {
                        StatusEffectViews.Remove(effectView);
                        Destroy(effectView);
                        return;
                    }
                }
            }
        }
    }
}
