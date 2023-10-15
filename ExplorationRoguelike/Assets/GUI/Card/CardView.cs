using System;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Combat.Events;
using System.Collections;
using Godot;

namespace ExplorationRoguelike.GUI.Card
{
    public partial class CardView : Node2D
    {
        [NonSerialized] public IPlayableCard playableCard;

        [Export] private Label cardNameText;

        [Export] private Label cardDescriptionText;

        [Export] private Label manaCostText;

        [Export] private float verticalMoveAmount = 30f;

        [Export] private float moveTime = 0.1f;

        [Export(PropertyHint.Range, "0,2,")] private float scaleAmount = 1.1f;

        private Vector2 _startPosition;
        private Vector2 _startScale;

        private int _index;

        [Export] private EventResource playCardEvent;

        public void Initialize(IPlayableCard playableCard)
        {
            this.playableCard = playableCard;

            // TODO: current position
            //_startPosition = position;
            // _startScale = transform.localScale;

            cardNameText.Text = this.playableCard.GameplayAbility.Name;
            cardDescriptionText.Text = this.playableCard.GameplayAbility.Description.ToString();
            manaCostText.Text = this.playableCard.ManaCost.ToString();
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
            while(elapsedTime < moveTime)
            {
                elapsedTime += GetProcessDeltaTime();
                if(startingAnimation)
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
}
