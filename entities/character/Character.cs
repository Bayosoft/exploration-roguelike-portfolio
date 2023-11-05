using ExplorationRoguelike.AbilitySystem;
using Godot;

namespace ExplorationRoguelike.Characters
{
    public partial class Character : Node2D
    {
        [Export]
        public CharacterResource CharacterResource { get; private set; }

        [Export] 
        public AbilitySystemComponent AbilitySystemComponent { get; private set; }
    }
}