using ExplorationRoguelike.GUI.PlayableCard;
using Godot;
using System;

public partial class CombatDeck : Node2D
{

    private Vector2 centerCardOval = DisplayServer.WindowGetSize() * new Vector2(0.5f, 1.3f);
    private float horizontalRadius = DisplayServer.WindowGetSize().X * 0.45f;
    private float verticalRadius = DisplayServer.WindowGetSize().Y * 0.4f;
    private float angle = Mathf.DegToRad(90);
    private Vector2 ovalAngleVector;
    

    public void OnDrawCard()
    {
        ovalAngleVector = new Vector2(horizontalRadius * Mathf.Cos(angle), -verticalRadius * Mathf.Sin(angle));
        // card.Position = centerCardoval + ovalAngleVector - card.Size/2;
        // card.Rotation = (90 - Mathf.RadToDeg(angle))/4; (rotates cards based on position in hand)
        // Change angle to place card on a different spot.
       
    }
}
