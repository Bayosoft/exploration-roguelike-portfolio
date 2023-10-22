using ExplorationRoguelike.AbilitySystem;
using Godot;

namespace ExplorationRoguelike.Characters
{
    public abstract partial class Character : Node
    {
        [Export] private CharacterResource characterData;

        public CharacterResource CharacterData
        {
            get => characterData;
            set => characterData = value;
        }

        [Export] private AbilitySystemComponent abilitySystemComponent;
        public AbilitySystemComponent AbilitySystemComponent => abilitySystemComponent;
    }
}