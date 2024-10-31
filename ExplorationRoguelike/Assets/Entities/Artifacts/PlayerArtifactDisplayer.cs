using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.GameplayEffects;
using ExplorationRoguelike.GameplayTags;
using ExplorationRoguelike.StatusEffect;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class PlayerArtifactDisplayer : MonoBehaviour
    {
        [SerializeField] protected GameObject artifactPrefab;

        [SerializeField] protected List<GameObject> artifactSlots;

        protected Dictionary<GameObject, GameObject> artifactsBySlot;
        public ObservableCollection<GameObject> Artifacts { get; set; }

        [SerializeField] protected GameplayTag[] excludedTags;

        public void Awake()
        {
            Artifacts = new ObservableCollection<GameObject>();
            artifactsBySlot = new Dictionary<GameObject, GameObject>();
            artifactSlots.ForEach(slot => artifactsBySlot.Add(slot, null));
        }
        public void Start()
        {
            DontDestroyOnLoad(transform.parent);
        }

        public void Initialize(Player player)
        {
            player.InventoryComponent.InventoryData.Artifacts.CollectionChanged += UpdateArtifacts;
        }

        public void UpdateArtifacts(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        Debug.Log("Artifact added" + ((Artifact)e.NewItems[0]).ArtifactName);
                        AddArtifact((Artifact)e.NewItems[0]);
                        //AddStatusEffect((ActiveGameplayEffect)e.NewItems[0]);
                        break;
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        //RemoveStatusEffect((ActiveGameplayEffect)e.OldItems[0]);
                        break;
                    }
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    //RemoveStatusEffect((ActiveGameplayEffect)e.OldItems[0]);
                    //AddStatusEffect((ActiveGameplayEffect)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void AddArtifact(Artifact addedEffect)
        {
            var freeSlot = artifactsBySlot.FirstOrDefault(slot => slot.Value == null);
            if (freeSlot.Equals(default(KeyValuePair<GameObject, GameObject>)))
            {
                // Dont visually add the status effects (TODO: Make it appear when a slot becomes available)
                return;
            }
            var artifactObject = Instantiate(artifactPrefab, freeSlot.Key.transform, true);
            artifactObject.transform.localScale = Vector2.one;
            artifactObject.transform.localPosition = Vector2.one;
            artifactObject.transform.SetAsLastSibling();
            var artifactComponent = artifactObject.GetComponent<ArtifactOverlay>();
            artifactComponent.Initialize(addedEffect);

            artifactsBySlot[freeSlot.Key] = artifactObject;
            // StatusEffects.Add(artifactObject);
        }
    }
}
