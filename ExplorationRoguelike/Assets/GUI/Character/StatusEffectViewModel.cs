using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayEffects;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike.GUI.StatusEffect
{
    public class StatusEffectViewModel : MonoBehaviour
    {
        [SerializeField]
        private GameObject statusEffectPrefab;

        [SerializeField]
        private AbilitySystemComponent characterAbilitySystem;

        public ObservableCollection<GameObject> StatusEffectViews { get; set; }

        public void Awake()
        {
            StatusEffectViews = new ObservableCollection<GameObject>();
        }

        public void Start()
        {
            characterAbilitySystem.ActiveGameplayEffects.ActiveEffects.CollectionChanged += new NotifyCollectionChangedEventHandler(PrintStatusEffect);
        }
        public void PrintStatusEffect(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                GameObject effectView = Instantiate(statusEffectPrefab);
                effectView.transform.parent = gameObject.transform;
                effectView.transform.localScale = Vector2.one;
                effectView.transform.localPosition = Vector2.one;
                effectView.transform.SetAsLastSibling();
                Character.StatusEffect effectComponent = effectView.GetComponent<Character.StatusEffect>();
                effectComponent.Initialize(((ActiveGameplayEffect)e.NewItems[0]).Specification.EffectSo);

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
