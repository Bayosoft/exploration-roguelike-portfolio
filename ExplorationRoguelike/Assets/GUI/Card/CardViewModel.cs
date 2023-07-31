using ExplortationRoguelike.GUI;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExplorationRoguelike.GUI.Card
{
    public class CardViewModel : ViewModel, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        public CardAbility Ability;

        [SerializeField]
        private TextMeshProUGUI _cardNameText;
        [SerializeField]
        private TextMeshProUGUI _cardDescriptionText;
        [SerializeField]
        private TextMeshProUGUI _manaCostText;

        [SerializeField]
        private float _verticalMoveAmount = 30f;
        [SerializeField]
        private float _moveTime = 0.1f;
        [Range(0f, 2f), SerializeField]
        private float _scaleAmount = 1.1f;

        private Vector3 _startPosition;
        private Vector3 _startScale;

        private int index;

        [SerializeField]
        private ScriptableEvent _playCardEvent;

        public void Initialize(CardAbility ability)
        {
            Ability = ability;

            _startPosition = transform.position;
            _startScale = transform.localScale;

            _cardNameText.text = Ability.Name;
            _cardDescriptionText.text = Ability.ToString();
            _manaCostText.text = Ability.ManaCost.ToString();
        }

        public void TryPlayCard()
        {
            _playCardEvent.RaiseEvent(new TryPlayCardEventArgs(Ability));
        }

        private IEnumerator HighlightCard(bool startingAnimation)
        {
            Vector3 endPosition;
            Vector3 endScale;

            float elapsedTime = 0f;
            while(elapsedTime < _moveTime)
            {
                elapsedTime += Time.deltaTime;
                if(startingAnimation)
                {
                    //endPosition = _startPosition + new Vector3(0, _verticalMoveAmount, 0);
                    endScale = _startScale * _scaleAmount;
                }
                else
                {
                  //  endPosition = _startPosition;
                    endScale = _startScale;
                }

             //   Vector3 lerpedPos = Vector3.Lerp(transform.position, endPosition, (elapsedTime / _moveTime));
                Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / _moveTime));

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
            index = gameObject.transform.GetSiblingIndex();
            gameObject.transform.SetAsLastSibling();

            StartCoroutine(HighlightCard(true));
        }

        public void OnDeselect(BaseEventData eventData)
        {
            gameObject.transform.SetSiblingIndex(index);
            StartCoroutine(HighlightCard(false));
        }

    }
}
