using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace CustomDatePicker
{
    public abstract class TweenRectBase : MonoBehaviour
    {
        [SerializeField] protected RectTransform rect;
        private Vector2 originalSize;
        [HideInInspector]
        public Vector2 originalPosition;

        public float maxScale = 1.5f;
        public float duration = 1f;
        public LoopType loopType = LoopType.Yoyo;

        void Awake()
        {
            if (rect == null) rect = GetComponent<RectTransform>();
            originalSize = rect.localScale;
            originalPosition = rect.anchoredPosition;
        }

        protected void OnEnable()
        {
            rect.localScale = originalSize;
            rect.anchoredPosition = originalPosition;
            DoAnimate();
        }

        protected void OnDisable()
        {
            rect.DOKill();
        }


        protected abstract void DoAnimate();
    }
}
