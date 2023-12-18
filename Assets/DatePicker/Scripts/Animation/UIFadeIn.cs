using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CustomDatePicker
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIFadeIn : MonoBehaviour
    {
        public float duration = 0.5f;
        public bool playOnAwake;
        public Ease EaseType = Ease.Linear;
        public UnityEvent OnAnimationComplete;


        private float usableDuration;
        private CanvasGroup thisCanvas;



        private void Awake()
        {
            thisCanvas = GetComponent<CanvasGroup>();
            usableDuration = duration;
            if (playOnAwake)
            {
                Animate();
            }

        }

        public void Skip()
        {
            thisCanvas.DOComplete();
        }

        [ContextMenu("Animate")]
        public void Animate()
        {
            thisCanvas.alpha = 0f;
            thisCanvas.DOFade(1f, usableDuration).SetEase(EaseType).OnComplete(() =>
            {
                OnAnimationComplete?.Invoke();
            });
            usableDuration = duration;

        }

        public void Animate(float timeDuration)
        {
            usableDuration = timeDuration;
            Animate();
        }
    }
}
