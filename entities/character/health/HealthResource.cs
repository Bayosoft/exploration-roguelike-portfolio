using Godot;

namespace ExplorationRoguelike.Characters
{
    [GlobalClass]
    public partial class HealthResource : Resource
    {
        [Export]
        public int MaxHealth { get; set; }
    }
}
