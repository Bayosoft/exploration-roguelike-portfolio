using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayEffects;
using Godot;

namespace ExplorationRoguelike.GUI.Characters
{
    public partial class StatusEffectViewModel : Node2D
    {
        [Export] private Resource statusEffectScene;

        private AbilitySystemComponent _characterAbilitySystem;

        public ObservableCollection<Node> StatusEffectViews { get; set; }

        public void Awake()
        {
            StatusEffectViews = new ObservableCollection<Node>();
        }
        
        public void Start()
        {
            if (_characterAbilitySystem == null)
            {
                // TODO: Get Player's ability system? (mind the likely rename to Action system)
               // _characterAbilitySystem = GameObject.FindObjectOfType<Player>().AbilitySystemComponent;
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
            var scene = (PackedScene)ResourceLoader.Load(statusEffectScene.ResourcePath);
            var statusEffectNode = scene.Instantiate();

            // TODO: Change status effect's position?
            /*statusEffectNode..localScale = Vector2.one;
            statusEffectNode.transform.localPosition = Vector2.one;
            statusEffectNode.transform.SetAsLastSibling();*/

            // TODO: Initialize the actual status effect
            // var effectComponent = effectView.GetComponent<StatusEffect>();
            // effectComponent.Initialize(addedEffect);

            StatusEffectViews.Add(statusEffectNode);
        }

        private void RemoveStatusEffectView(ActiveGameplayEffect removedEffect)
        {
            foreach (var effectView in
                     StatusEffectViews.ToList()
                         .Where(x => true /* TODO: Get status effect and check if it is the removed effect. effectView => effectView.GetComponent<StatusEffect>().GameplayEffect == removedEffect*/))
            {
                StatusEffectViews.Remove(effectView);
                effectView.QueueFree();
                return;
            }

        }
    }
}