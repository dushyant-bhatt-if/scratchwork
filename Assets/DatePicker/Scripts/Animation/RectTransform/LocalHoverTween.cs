using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace CustomDatePicker
{
    public class LocalHoverTween : TweenRectBase
    {


        protected override void DoAnimate()
        {
            float calculatedTime = Random.Range(1f, 1.3f);
            rect.DOAnchorPosY(maxScale, calculatedTime).SetLoops(-1, loopType);
        }
    }
}
