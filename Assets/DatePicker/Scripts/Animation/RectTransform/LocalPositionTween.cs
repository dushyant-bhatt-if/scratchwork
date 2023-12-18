using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace CustomDatePicker
{
    public class LocalPositionTween : TweenRectBase
    {
        public Ease ease = Ease.Flash;
        public float transitionDuration = .6f;
        protected override void DoAnimate()
        {
            rect.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y - 250f);
            rect.DOAnchorPosY(originalPosition.y, transitionDuration).SetEase(ease);
        }
    }
}
