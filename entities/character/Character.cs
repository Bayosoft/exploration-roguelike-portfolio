using ExplorationRoguelike.AbilitySystem;
using Godot;

namespace ExplorationRoguelike.Characters
{
    public abstract partial class Character : Node
    {
        [Export] private CharacterResource characterResource;

        public CharacterResource CharacterResource
        {
            get => characterResource;
            set => characterResource = value;
        }

        [Export] private AbilitySystemComponent abilitySystemComponent;
        public AbilitySystemComponent AbilitySystemComponent => abilitySystemComponent;
    }
}