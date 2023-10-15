using ExplorationRoguelike.AbilitySystem.Abilities;
using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace ExplorationRoguelike.Characters
{
    public abstract partial class CharacterResource : Resource
    {
        [Export]
        private string name;
        public string Name => name;

        [Export]
        private Sprite2D gameImage;
        public Sprite2D GameImage => gameImage;

        [Export]
        private Sprite2D uiImage;
        public Sprite2D UiImage => uiImage;

        [Export]
        private Array<GameplayAbility> abilities;
        public Array<GameplayAbility> Abilities => abilities;

        [Export]
        private HealthData health;
        public virtual HealthData Health => health;
    }
}
