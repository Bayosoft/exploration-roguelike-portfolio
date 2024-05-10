using System;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.GameplayTags;

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

    public enum GameplayModifierDirection
    {
        Incoming,
        Outgoing
    }

    [Serializable]
    public struct GameplayModifier
    {
        public GameplayTagRequirements tagRequirements;
        public GameplayModifierOperator @operator;
        public GameplayModifierDirection direction;
        public float modifierMagnitude;
        public GameplayTagContainer modifierTags;

        // True if the instigator modifiers are added to the magnitude are snapshotted
        public bool snapshotInstigator;

        public bool DoesMeetRequirements(GameplayTagContainer tags, GameplayModifierDirection abilityDirection, GameplayTagContainer dynamicTags = null)
        {
            if(direction != abilityDirection)
            {
                return false;
            }

            if(dynamicTags == null)
            {
                return tagRequirements.RequirementsMet(tags);
            }
            else
            {
                return tagRequirements.RequirementsMet(tags, dynamicTags);
            }
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

            if(modifier.snapshotInstigator)
            {
                _snapshottedMagnitude = modifier.modifierMagnitude;

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
        public bool TryApply(ref float value, GameplayTagContainer valueTags, GameplayTagContainer dynamicTags, GameplayModifierDirection direction, AbilitySystemComponent instigator, AbilitySystemComponent target)
        {
            if (_modifierBase.DoesMeetRequirements(valueTags, direction, dynamicTags) == false)
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
                magnitude = _modifierBase.modifierMagnitude;

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

            switch (_modifierBase.@operator)
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
