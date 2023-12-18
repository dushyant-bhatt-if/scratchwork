using DG.Tweening;
using UnityEngine;
namespace CustomDatePicker
{
    public class TransformHooverTween : TweenTransformBase
    {
        public Vector3 direction = Vector3.up;

        protected override void DoAnimate()
        {
            trans.DOLocalMove(direction, duration).SetEase(easeType).SetLoops(-1, loopType);
        }
    }
}
