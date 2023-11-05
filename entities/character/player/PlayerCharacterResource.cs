using Godot;

namespace ExplorationRoguelike.Characters.PlayerCharacter
{
    [GlobalClass]
    public partial class PlayerCharacterResource : CharacterResource
    {

        [Export]
        private Sprite2D playerFrontSprite;
        public Sprite2D PlayerFrontSprite => playerFrontSprite;
    }
}
