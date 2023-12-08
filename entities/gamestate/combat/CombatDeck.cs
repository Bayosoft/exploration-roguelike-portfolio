using ExplorationRoguelike.AbilitySystem.CardSystem;
using ExplorationRoguelike.Combat;
using ExplorationRoguelike.GUI.PlayableCard;
using Godot;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public partial class CombatDeck : Node2D
{
    [Export] private Vector2 cardSize;
    [Export] private Node drawnCards;
    public Card SelectedCard { get; set; }
    public ObservableCollection<Card> Cards { get; set; }

    private Vector2 centerCardOval = DisplayServer.WindowGetSize() * new Vector2(0.5f, 1.25f);
    private float horizontalRadius = DisplayServer.WindowGetSize().X * 0.45f;
    private float verticalRadius = DisplayServer.WindowGetSize().Y * 0.4f;
    private float angle = Mathf.DegToRad(90);
    private Vector2 ovalAngleVector;

    public CombatDeck()
    {
        Cards = new ObservableCollection<Card>();
    }
    public void Initialize(CardDeckComponent cardDeckComponent)
    {
        cardDeckComponent.CardsDrawn.CollectionChanged += UpdateDrawnCards;
    }

    public void UpdateDrawnCards(object sender, NotifyCollectionChangedEventArgs e)
    {
        //different kind of changes that may have occurred in collection
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            Card card = (Card)e.NewItems[0];

            drawnCards.AddChild(card);
            card.SetSize(cardSize);

            ovalAngleVector = new Vector2(horizontalRadius * Mathf.Cos(angle), -verticalRadius * Mathf.Sin(angle));

            GD.Print(DisplayServer.WindowGetSize().X / 2);
            GD.Print(drawnCards.GetChildren().Count * 100);
            GD.Print(card.GetIndex() * 100);
            GD.Print((DisplayServer.WindowGetSize().X / 2) - (drawnCards.GetChildren().Count * 100) + (card.GetIndex() * 100));
            card.Position = 
                new Vector2((DisplayServer.WindowGetSize().X / 2) - (drawnCards.GetChildren().Count * 100) + (card.GetIndex() * 200), 700);
            // card.Position = centerCardOval + ovalAngleVector - card.Size/2;

            // card.Rotation = (90 - Mathf.RadToDeg(angle))/4; (rotates cards based on position in hand)
            // Change angle to place card on a different spot.

            Cards.Add(card);
        }
        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (Card removedCard in e.OldItems)
            {
                drawnCards.RemoveChild(removedCard);               
            }
        }
     
    }
}
