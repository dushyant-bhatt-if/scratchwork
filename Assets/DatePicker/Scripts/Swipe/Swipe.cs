using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
namespace CustomDatePicker
{
    public class Swipe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
    {


        [Header("UI")]
        [SerializeField] private GameObject dragTarget;
        [SerializeField] private GameObject swipeTarget;
        [SerializeField] private bool PlayOnAwake;
        private RectTransform TargetrectTransform;

        [Header("Configuration")]
        public bool swipable = true;
        public SwipeDirection swipeDirection = SwipeDirection.Horizontal;
        public float distance = 40f;
        public float duration = 1f;
        public float acceptableThresold = .7f;
        public bool bothDirection = false;
        public bool clickable = true;

        private Vector2 _firstPosition;

        private int direction = -1;
        private float dragAmountXAxis = 0;
        private float dragAmountYAxis = 0;

        private bool firstLoad = true;
        public UnityEvent onClick;
        public UnityEvent swipeInFinished;
        public UnityEvent swipeStart;
        public UnityEvent swipeOutFinished;
        private bool open;

        private void Awake()
        {
            TargetrectTransform = swipeTarget.GetComponent<RectTransform>();
            _firstPosition = TargetrectTransform.anchoredPosition;

        }

        private void OnEnable()
        {

            if (TargetrectTransform != null) swipeTarget.GetComponent<RectTransform>().anchoredPosition = _firstPosition;
            if (PlayOnAwake)
            {
                if (swipeDirection == SwipeDirection.Horizontal) TargetrectTransform.anchoredPosition = new Vector2(_firstPosition.x + distance, _firstPosition.y);
                else TargetrectTransform.anchoredPosition = new Vector2(_firstPosition.x, _firstPosition.y + distance);
            }
        }
        private void OnDisable()
        {
            TargetrectTransform.DOKill();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!swipable) return;
            if (firstLoad)
            {
                _firstPosition = TargetrectTransform.anchoredPosition;
                firstLoad = false;
            }
            swipeTarget.SetActive(true);
            dragAmountXAxis = 0;
            dragAmountYAxis = 0;

        }

        //todo check for quality
        public void OnDrag(PointerEventData eventData)
        {
            if (!swipable) return;
            dragAmountXAxis += eventData.delta.x;
            dragAmountYAxis += eventData.delta.y;
            if (swipeDirection == SwipeDirection.Horizontal && bothDirection)
            {
                TargetrectTransform.anchoredPosition += new Vector2(eventData.delta.x, 0);
                float x = Mathf.Clamp(TargetrectTransform.anchoredPosition.x, _firstPosition.x - distance, _firstPosition.x + distance);
                TargetrectTransform.anchoredPosition = new Vector2(x, TargetrectTransform.anchoredPosition.y);
            }
            else if (swipeDirection == SwipeDirection.Horizontal)
            {
                TargetrectTransform.anchoredPosition += new Vector2(eventData.delta.x, 0);
                float x = Mathf.Clamp(TargetrectTransform.anchoredPosition.x, _firstPosition.x - distance, _firstPosition.x);
                TargetrectTransform.anchoredPosition = new Vector2(x, TargetrectTransform.anchoredPosition.y);
            }
            else
            {

                TargetrectTransform.anchoredPosition += new Vector2(0, eventData.delta.y);
                float y = Mathf.Clamp(TargetrectTransform.anchoredPosition.y, _firstPosition.y - distance, _firstPosition.y);
                TargetrectTransform.anchoredPosition = new Vector2(TargetrectTransform.anchoredPosition.x, y);
            }
            swipeStart.Invoke();

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            dragAmountXAxis += eventData.delta.x;
            dragAmountYAxis += eventData.delta.y;
            MoveToTargetPosition();
        }

        private void MoveToTargetPosition()
        {
            if (!swipable) return;
            if (bothDirection && Mathf.Abs(dragAmountXAxis) > Mathf.Abs(dragAmountYAxis) && Mathf.Abs(dragAmountXAxis) > Mathf.Abs(distance) * acceptableThresold && swipeDirection == SwipeDirection.Horizontal)
            {
                if (dragAmountXAxis > 0 && Vector2.Distance(_firstPosition, TargetrectTransform.anchoredPosition) >= distance * acceptableThresold)
                {
                    TargetrectTransform.DOAnchorPosX(_firstPosition.x + distance, duration);
                    direction *= -1;
                    Debug.Log("swipe out");
                    StartCoroutine(Wait(.1f, () =>
                    {
                        swipeOutFinished.Invoke();
                    }));

                }
                else if (dragAmountXAxis <= 0 && Vector2.Distance(_firstPosition, TargetrectTransform.anchoredPosition) >= distance * acceptableThresold)
                {
                    TargetrectTransform.DOAnchorPosX(_firstPosition.x - distance, duration);
                    direction *= -1;
                    Debug.Log("swipe in");
                    StartCoroutine(Wait(.1f, () =>
                    {
                        swipeInFinished.Invoke();
                    }));

                }
                else
                {
                    Debug.Log("swipe in:" + Vector2.Distance(_firstPosition, TargetrectTransform.anchoredPosition));
                    TargetrectTransform.DOAnchorPos(new Vector2(_firstPosition.x, _firstPosition.y), duration);
                }

            }
            else if (bothDirection && Mathf.Abs(dragAmountYAxis) > Mathf.Abs(dragAmountXAxis) && Mathf.Abs(dragAmountYAxis) > Mathf.Abs(distance) * acceptableThresold && swipeDirection == SwipeDirection.Vertical)
            {

                if (dragAmountYAxis > 0 && direction == 1)
                {

                    TargetrectTransform.DOAnchorPosY(_firstPosition.y, duration);
                    direction *= -1;
                }
                else if (direction == -1)
                {
                    TargetrectTransform.DOAnchorPosY(_firstPosition.y + distance, duration);
                    direction *= -1;
                }

            }
            else if (Mathf.Abs(dragAmountXAxis) > Mathf.Abs(dragAmountYAxis) && Mathf.Abs(dragAmountXAxis) > Mathf.Abs(distance) * acceptableThresold && swipeDirection == SwipeDirection.Horizontal)
            {
                if (dragAmountXAxis > 0)
                {
                    TargetrectTransform.DOAnchorPosX(_firstPosition.x, duration);
                    direction *= -1;
                }
                else
                {
                    TargetrectTransform.DOAnchorPosX(_firstPosition.x - distance, duration);
                    direction *= -1;
                }
                open = true;
            }
            else if (Mathf.Abs(dragAmountYAxis) > Mathf.Abs(dragAmountXAxis) && Mathf.Abs(dragAmountYAxis) > Mathf.Abs(distance) * acceptableThresold && swipeDirection == SwipeDirection.Vertical)
            {
                if (dragAmountYAxis > 0)
                {
                    TargetrectTransform.DOAnchorPosY(_firstPosition.y + distance, duration);
                    StartCoroutine(Wait(.5f, () =>
                    {
                        swipeInFinished.Invoke();

                    }));
                }
                else
                {
                    TargetrectTransform.DOAnchorPosY(_firstPosition.y, duration);
                }

            }
            else
            {
                TargetrectTransform.DOAnchorPos(new Vector2(_firstPosition.x, _firstPosition.y), duration);
            }


        }
        private IEnumerator Wait(float time, Action action)
        {

            yield return new WaitForSecondsRealtime(time);
            action();


        }

        public void showSlide()
        {

            dragTarget.SetActive(true);
            CanvasGroup canvasGroup = dragTarget.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.DOKill();
                canvasGroup.alpha = 1;
            }
            if (TargetrectTransform != null)
            {
                TargetrectTransform.anchoredPosition = _firstPosition;
            }
            if (swipeDirection == SwipeDirection.Vertical)
            {
                TargetrectTransform.DOAnchorPosY(_firstPosition.y + distance, duration);
            }
            else
            {

                TargetrectTransform.DOAnchorPosX(_firstPosition.x - distance, duration);
            }
            direction *= -1;
            open = true;
        }

        public void hideSlide()
        {

            if (swipeDirection == SwipeDirection.Vertical)
            {

                TargetrectTransform.DOAnchorPosY(_firstPosition.y, duration).OnComplete(() =>
                {
                    swipeOutFinished.Invoke();
                });
            }
            else
            {
                TargetrectTransform.DOAnchorPosX(_firstPosition.x, duration).OnComplete(() =>
                {
                    swipeOutFinished.Invoke();
                });
            }
            direction *= -1;
            open = false;

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Clicked On:" + eventData.pointerEnter.gameObject.name + open);
            if (Mathf.Abs(dragAmountXAxis) == 0 && Mathf.Abs(dragAmountYAxis) == 0 && clickable && eventData.pointerEnter.gameObject.name == dragTarget.gameObject.name)
            {
                onClick.Invoke();
                if (Vector2.Distance(_firstPosition, TargetrectTransform.anchoredPosition) > 10 && open) hideSlide();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            dragAmountXAxis = 0;
            dragAmountYAxis = 0;
        }
        public void hideItem()
        {
            TargetrectTransform.anchoredPosition = _firstPosition;
            dragTarget.SetActive(false);

        }
    }
    public enum SwipeDirection
    {
        Horizontal,
        Vertical,
    }
}