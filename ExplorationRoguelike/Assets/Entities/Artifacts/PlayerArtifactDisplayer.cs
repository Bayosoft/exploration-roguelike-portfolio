using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.GameplayEffects;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class PlayerArtifactDisplayer : MonoBehaviour
    {
        //[SerializeField]
        // private List<GameObject> artifactPrefab
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

       /* protected override void AddStatusEffect(ActiveGameplayEffect addedEffect)
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
        }*/
    }
}
