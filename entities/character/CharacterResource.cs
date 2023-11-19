using ExplorationRoguelike.AbilitySystem.Abilities;
using Godot;
using Godot.Collections;

namespace ExplorationRoguelike.Characters
{
    [GlobalClass]
    public partial class CharacterResource : Resource
    {
        [Export]
        private string name;
        public string Name => name;

/*        [Export]
        private Sprite2D gameImage;
        public Sprite2D GameImage => gameImage;

        [Export]
        private Sprite2D uiImage;
        public Sprite2D UiImage => uiImage;*/

        [Export]
        private Array<GameplayAbility> abilities;
        public Array<GameplayAbility> Abilities => abilities;

        [Export]
        private HealthResource health;
        public virtual HealthResource Health => health;

        public void Spawn()
        {
            // TODO: Instantiate Node.
        }
    }
}
