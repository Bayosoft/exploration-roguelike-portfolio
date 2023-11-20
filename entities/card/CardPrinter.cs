using ExplorationRoguelike.AbilitySystem.Abilities;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace ExplorationRoguelike.GUI.PlayableCard;

// TODO: Potentially this should be a Node so that it can be attached to a scene parent for displaying cards.
public partial class CardPrinter : Resource
{
    [Export] public Resource CardScene;

    public IEnumerable<Card> PrintStackFromAbilities(IEnumerable<GameplayAbility> abilities)
    {
        List<Card> cards = new(); // Logic of the cards

        foreach (var ability in abilities.Cast<IPlayableCard>())
        {
            var scene = (PackedScene)ResourceLoader.Load(CardScene.ResourcePath);
            var cardControl = scene.Instantiate<Card>();
            cardControl.Initialize(ability);

            cards.Add(cardControl);
        }
        return cards;
    }
}
