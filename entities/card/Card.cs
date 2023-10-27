using System;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Combat.Events;
using System.Collections;
using Godot;

namespace ExplorationRoguelike.GUI.PlayableCard;
public partial class Card : Node2D
{
    [NonSerialized] public IPlayableCard PlayableCard;

    [Export] private RichTextLabel cardNameText;

    [Export] private RichTextLabel cardDescriptionText;

    [Export] private RichTextLabel manaCostText;

    [Export] private float verticalMoveAmount = 30f;

    [Export] private float moveTime = 0.1f;

    [Export(PropertyHint.Range, "0,2,")] private float scaleAmount = 1.1f;

    private Vector2 _startPosition;
    private Vector2 _startScale;

    private int _index;

    [Export] private EventResource playCardEvent;

    public void Initialize(IPlayableCard playableCard)
    {
        PlayableCard = playableCard;

        // TODO: current position
        //_startPosition = position;
        // _startScale = transform.localScale;

        cardNameText.Text = PlayableCard.GameplayAbility.Name;
        cardDescriptionText.Text = PlayableCard.GameplayAbility.Description.ToString();
        manaCostText.Text = PlayableCard.ManaCost.ToString();
    }

    public void TryPlayCard()
    {
        playCardEvent.RaiseEvent(new TryPlayCardEventArgs(this));
    }

    private IEnumerator HighlightCard(bool startingAnimation)
    {
        Vector2 endPosition;
        Vector2 endScale;

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

            // TODO: Transition to Godot Lerp
            // Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));

            //  Scale = lerpedScale;

            yield return null;
        }
    }

    // TODO: Create node with button Signals.
    public void OnPointerEnter()
    {
        //   eventData.selectedObject = gameObject;
    }

    public void OnPointerExit()
    {
        //   eventData.selectedObject = null;
    }

    public void OnSelect()
    {
        /*  _index = gameObject.transform.GetSiblingIndex();
          gameObject.transform.SetAsLastSibling();

          StartCoroutine(HighlightCard(true));*/
    }
    public void OnDeselect()
    {
        /*            gameObject.transform.SetSiblingIndex(_index);
                    StartCoroutine(HighlightCard(false));*/
    }
    public void OnPointerClick()
    {
        TryPlayCard();
    }
}
