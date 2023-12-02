using System;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Combat.Events;
using System.Collections;
using Godot;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ExplorationRoguelike.GUI.PlayableCard;
public partial class Card : Control
{
    [NonSerialized] public IPlayableCard PlayableCard;

    [Export] private RichTextLabel cardNameText;

    [Export] private RichTextLabel cardDescriptionText;

    [Export] private RichTextLabel manaCostText;

    [Export] private float verticalMoveAmount = 30f;

    [Export] private float moveTime = 3f;

    [Export(PropertyHint.Range, "0,2,")] private float scaleAmount = 1.1f;

    private Vector2 _startPosition;
    private Vector2 _startScale;

    private int _index;

    [Export] private EventResource playCardEvent;

    public void Initialize(IPlayableCard playableCard)
    {
        PlayableCard = playableCard;

        cardNameText.Text = PlayableCard.GameplayAbility.Name;
        cardDescriptionText.Text = PlayableCard.GameplayAbility.Description?.ToString();
        manaCostText.Text = PlayableCard.ManaCost.ToString();
    }

    public void TryPlayCard()
    {
        playCardEvent.RaiseEvent(new TryPlayCardEventArgs(this));
    }

    private void HighlightCard(bool startingAnimation)
    {
        Vector2 endPosition;
        Vector2 endScale;
        Vector2 lerpedScale;

        double elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            elapsedTime += GetProcessDeltaTime();
            if (startingAnimation)
            {
                endScale = _startScale * scaleAmount;
            }
            else
            {
                endScale = _startScale;
            }

            lerpedScale = Scale.Lerp(endScale, (float)(elapsedTime / moveTime));
            //Debug.WriteLine(lerpedScale);

            Scale = lerpedScale;
        }
    }

    public void OnPointerEnter()
    {
        _startPosition = Position;
        _startScale = Scale;
        ZIndex = 1;

        HighlightCard(true);
        // _startScale = transform.localScale;
    }

    public void OnPointerExit()
    {
        ZIndex = 0;
        HighlightCard(false);
        //   eventData.selectedObject = null;
    }

    public void OnPointerClick()
    {
        TryPlayCard();
    }
}
