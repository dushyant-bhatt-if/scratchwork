using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace CustomDatePicker
{
    public class Popup : MonoBehaviour
    {
        public string popupName;
        [SerializeField] private Direction direction = Direction.Horizontal;
        [SerializeField] private float distance = 40f;
        [SerializeField] private float duration = 1f;
        private RectTransform rectTransform;
        private Vector2 initialPosition;
        private void Awake()
        {

        }
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            initialPosition = rectTransform.anchoredPosition;
            PopupManager.Instance?.Register(this);
        }

        public void ShowPopup()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
                initialPosition = rectTransform.anchoredPosition;
            }
            rectTransform.DOKill();
            gameObject.SetActive(true);
            if (direction == Direction.Horizontal) rectTransform.DOAnchorPosX(initialPosition.x + distance, duration).OnComplete(() =>
            {
                PopupManager.Instance.OnAnimComplete?.Invoke();
            });
            else rectTransform.DOAnchorPosY(initialPosition.y + distance, duration).OnComplete(() =>
            {
                gameObject.SetActive(true);
                PopupManager.Instance.OnAnimComplete?.Invoke();
            });
            if (PopupManager.Instance != null && !popupName.Equals("bottom_circular_menu")) PopupManager.Instance.blurBackground.gameObject.SetActive(true);
            PopupManager.Instance?.currentOpenPopup.Add(this);
        }
        public void HidePopup()
        {
            rectTransform.DOKill();
            if (direction == Direction.Horizontal)
            {
                rectTransform.DOAnchorPosX(initialPosition.x, duration).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    PopupManager.Instance.OnAnimComplete?.Invoke();
                });
            }
            else
            {
                rectTransform.DOAnchorPosY(initialPosition.y, duration).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    PopupManager.Instance.OnAnimComplete?.Invoke();
                });
            }
            PopupManager.Instance.currentOpenPopup.Remove(this);
            if (PopupManager.Instance.currentOpenPopup.Count == 0) PopupManager.Instance.blurBackground.gameObject.SetActive(false);
        }
        public void ShowPopupImmediate()
        {
            rectTransform.anchoredPosition = new Vector2(initialPosition.x, initialPosition.y + distance);
        }
    }

    public enum Direction
    {
        Horizontal,
        Vertical,
    }
}

