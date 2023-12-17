using System;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Combat.Events;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExplorationRoguelike.GUI.Card
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler
    {
        [NonSerialized] public IPlayableCard playableCard;

        [SerializeField]
        private TextMeshProUGUI cardNameText;
        [SerializeField]
        private TextMeshProUGUI cardDescriptionText;
        [SerializeField]
        private TextMeshProUGUI manaCostText;

        [SerializeField]
        private float verticalMoveAmount = 30f;
        [SerializeField]
        private float moveTime = 0.1f;
        [Range(0f, 2f), SerializeField]
        private float scaleAmount = 1.1f;

        private Vector3 _startPosition;
        private Vector3 _startScale;

        private int _index;

        [SerializeField]
        private ScriptableEvent playCardEvent;

        public void Initialize(IPlayableCard playableCard)
        {
            this.playableCard = playableCard;

            _startPosition = transform.position;
            _startScale = transform.localScale;

            cardNameText.text = this.playableCard.GameplayAbility.name;
            cardDescriptionText.text = this.playableCard.GameplayAbility.Description.ToString();
            manaCostText.text = this.playableCard.ManaCost.ToString();
        }

        public void TryPlayCard()
        {
            playCardEvent.RaiseEvent(new TryPlayCardEventArgs(this));
        }

        private IEnumerator HighlightCard(bool startingAnimation)
        {
            Vector3 endPosition;
            Vector3 endScale;

            float elapsedTime = 0f;
            while(elapsedTime < moveTime)
            {
                elapsedTime += Time.deltaTime;
                if(startingAnimation)
                {
                    //endPosition = _startPosition + new Vector3(0, _verticalMoveAmount, 0);
                    endScale = _startScale * scaleAmount;
                }
                else
                {
                  //  endPosition = _startPosition;
                    endScale = _startScale;
                }

                //   Vector3 lerpedPos = Vector3.Lerp(transform.position, endPosition, (elapsedTime / _moveTime));
                Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));

             //   transform.position = lerpedPos;
                transform.localScale = lerpedScale;

                yield return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject = null;
        }
        public void OnSelect(BaseEventData eventData)
        {
            _index = gameObject.transform.GetSiblingIndex();
            gameObject.transform.SetAsLastSibling();

            StartCoroutine(HighlightCard(true));
        }
        public void OnDeselect(BaseEventData eventData)
        {
            gameObject.transform.SetSiblingIndex(_index);
            StartCoroutine(HighlightCard(false));
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            TryPlayCard();
        }
    }
}
