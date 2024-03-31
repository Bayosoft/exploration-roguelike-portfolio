using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using NUnit.Framework;
using System;
using UnityEngine;

namespace ExplorationRoguelike
{
    public abstract class DialogueOption : ScriptableObject
    {
        public string OptionText;
        public GameplayAbility dialogueAbility;
        [SerializeField]
        private CharacterData instigator;

        [SerializeField]
        private CharacterData target;

        public virtual void Activate()
        {
            if (dialogueAbility != null)
            {
                AbilitySystemComponent instigatorAsc = null;
                AbilitySystemComponent targetAsc = null;

                if (target != null || instigator != null)
                {
                    Character[] characters = FindObjectsByType<Character>(FindObjectsSortMode.None);

                    foreach (Character character in characters)
                    {
                        if(character.CharacterData == instigator)
                        {
                            instigatorAsc = character.AbilitySystemComponent;
                        }
                        if(character.CharacterData == target)
                        {
                            targetAsc = character.AbilitySystemComponent;
                        }
                    }
                }
                dialogueAbility.Activate(instigatorAsc, targetAsc);
            }
        }
    }
}