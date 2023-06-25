using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public enum GameplayModifierOperator
    {
        // NYI
        Override,
        // Add or subtract from the value
        Additive,
        Multiplication,
        Division
    }

    [Serializable]
    public struct GameplayModifier
    {
        public GameplayTagRequirements TagRequirements;
        public GameplayModifierOperator Operator;
        public float ModifierMagnitude;
        public GameplayTagContainer ModifierTags;

        // True if the instigator modifiers are added to the magnitude are snapshotted
        public bool SnapshotInstigator;

        public bool DoesMeetRequirements(in GameplayTagContainer tags, in GameplayTagContainer dynamicTags)
        {
            return TagRequirements.RequirementsMet(tags, dynamicTags);
        }

        public GameplayModifierSpec MakeModifierSpec()
        {
            GameplayModifierSpec spec = new GameplayModifierSpec();

            return spec;
        }
    }

    // An instanced, runtime modifiable, version of GameplayModifier
    [Serializable]
    public struct GameplayModifierSpec
    {
        private GameplayModifier _modifierBase;
        private float _snapshottedMagnitude;
        private bool _didSnapshotInstigator;

        public GameplayModifierSpec(GameplayModifier modifier, AbilitySystemComponent instigator)
        {
            _modifierBase = modifier;

            if(modifier.SnapshotInstigator)
            {
                _snapshottedMagnitude = modifier.ModifierMagnitude;

                GameplayTag outgoingTag = null;
                instigator.CalculateAggregatedModifiers(ref _snapshottedMagnitude, modifier.ModifierTags, outgoingTag.ToSingleTagContainer());

                _didSnapshotInstigator = true;
            }
            else
            {
                _snapshottedMagnitude = 0f;
                _didSnapshotInstigator = false;
            }
        }
        
        // Return true if it passed the required tags to modify the value
        public bool TryApply(ref float value, GameplayTagContainer valueTags, GameplayTagContainer dynamicTags, AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            if (_modifierBase.DoesMeetRequirements(valueTags, dynamicTags) == false)
            {
                return false;
            }

            Apply(ref value, instigator, target);
            return true;
        }

        void Apply(ref float value, AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            float magnitude = 0f;

            if(_didSnapshotInstigator)
            {
                magnitude = _snapshottedMagnitude;
            }
            else
            {
                // base magnitude
                magnitude = _modifierBase.ModifierMagnitude;

                // Apply instigator mods
                if(instigator != null)
                {
                    // @TODO: access GameplayTagsLibrary to retrieve OutgoingTag
                    GameplayTag outgoingTag = null;
                    instigator.CalculateAggregatedModifiers(ref magnitude, _modifierBase.ModifierTags, outgoingTag.ToSingleTagContainer());
                }
            }

            // Apply target mods
            if(target != null)
            {
                // @TODO: access GameplayTagsLibrary to retrieve IncomingTag
                GameplayTag incomingTag = null;
                target.CalculateAggregatedModifiers(ref magnitude, _modifierBase.ModifierTags, incomingTag.ToSingleTagContainer());
            }

            switch (_modifierBase.Operator)
            {
                case GameplayModifierOperator.Override:
                    break;
                case GameplayModifierOperator.Additive:
                    value += magnitude;
                    break;
                case GameplayModifierOperator.Multiplication:
                    value *= magnitude;
                    break;
                case GameplayModifierOperator.Division:
                    value /= magnitude;
                    break;
                default:
                    break;
            }
        }
    }
}
