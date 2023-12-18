
using DG.Tweening;
namespace CustomDatePicker
{
    public class LocalScaleTween : TweenRectBase
    {

        protected override void DoAnimate()
        {

            rect.DOScale(maxScale, duration).SetLoops(-1, loopType);
        }
    }
}
