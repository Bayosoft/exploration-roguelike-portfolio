using ExplorationRoguelike.GameplayTags;
using Godot;

[GlobalClass]
public partial class RaceResource : Resource
{
    [Export]
    public GameplayTag RaceTag { get; set; }
}
