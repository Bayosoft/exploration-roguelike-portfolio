using ExplorationRoguelike.AbilitySystem.Abilities;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace ExplorationRoguelike.GUI.PlayableCard;

// TODO: Potentially this should be a Node so that it can be attached to a scene parent for displaying cards.
public partial class CardPrinter : Resource
{
    [Export] public Resource CardScene;
    [Export] public Vector2 CardSize;
    public IEnumerable<Card> PrintStackFromAbilities(IEnumerable<GameplayAbility> abilities)
    {
        List<Card> cards = new(); // Logic of the cards

        foreach (var ability in abilities.Cast<IPlayableCard>())
        { 
            var scene = (PackedScene)ResourceLoader.Load(CardScene.ResourcePath);
            var cardNode = scene.Instantiate<Card>();
            cardNode.Initialize(ability);
            cardNode.SetSize(CardSize/cardNode.Scale);
            cardNode.SetPosition(new Vector2(500, 500));
            // TODO: After instantiating, it should be set as child of a node so that they can be spawned in scene. 

            // TODO: Move this functionality to parent node of card.
/*                var cardComponent = cardNode.GetChild<CardView>();
            cardComponent.Initialize(ability);*/

           cards.Add(cardNode);
        }
        return cards;
    }
}
