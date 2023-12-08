using System;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayTags;
using Godot;

namespace ExplorationRoguelike.GameplayEffects
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

    [GlobalClass]
    public partial class GameplayModifier : Resource
    {
        [Export] public GameplayTagRequirements TagRequirements;
        [Export] public GameplayModifierOperator Operator;
        [Export] public float ModifierMagnitude;
        [Export] public GameplayTagContainer ModifierTags;

        // True if the instigator modifiers are added to the magnitude are snapshotted
        [Export] public bool snapshotInstigator;

        public bool DoesMeetRequirements(GameplayTagContainer tags, GameplayTagContainer dynamicTags = null)
        {
            if(dynamicTags == null)
            {
                return TagRequirements.RequirementsMet(tags);
            }
            else
            {
                return TagRequirements.RequirementsMet(tags, dynamicTags);
            }
        }

        public GameplayModifierSpec MakeModifierSpec()
        {
            GameplayModifierSpec spec = new();

            return spec;
        }
    }

    // An instanced, runtime modifiable, version of GameplayModifier
    public class GameplayModifierSpec
    {
        private GameplayModifier _modifierBase;
        private float _snapshottedMagnitude;
        private bool _didSnapshotInstigator;

        public GameplayModifierSpec()
        {
            
        }
        public GameplayModifierSpec(GameplayModifier modifier, AbilitySystemComponent instigator)
        {
            _modifierBase = modifier;

            if(modifier.snapshotInstigator)
            {
                _snapshottedMagnitude = modifier.ModifierMagnitude;

                // @TODO: access GameplayTagsLibrary to retrieve IncomingTag for dynamic tags
                GameplayTag outgoingTag = null;
//                _snapshottedMagnitude = instigator.CalculateAggregatedModifiers(_snapshottedMagnitude, modifier.ModifierTags);

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
                    // @TODO: access GameplayTagsLibrary to retrieve OutgoingTag for dynamic tags
                    GameplayTag outgoingTag = null;
//                    magnitude = instigator.CalculateAggregatedModifiers(magnitude, _modifierBase.ModifierTags);
                }
            }

            // Apply target mods
            if(target != null)
            {
                // @TODO: access GameplayTagsLibrary to retrieve IncomingTag for dynamic tags
                GameplayTag incomingTag = null;
//                magnitude = target.CalculateAggregatedModifiers(magnitude, _modifierBase.ModifierTags);
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
