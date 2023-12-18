using DG.Tweening;

namespace CustomDatePicker
{
    public class TransformScaleTween : TweenTransformBase
    {
        protected override void DoAnimate()
        {
            trans.DOScale(maxScale, duration).SetEase(easeType).SetLoops(-1, loopType);
        }
    }
}