using DG.Tweening;
using UnityEngine;
namespace CustomDatePicker
{
    public abstract class TweenTransformBase : MonoBehaviour
    {
        [SerializeField] protected Transform trans;
        private Vector3 originalSize;
        [HideInInspector]
        public Vector3 originalPosition;

        public float maxScale = 1.5f;
        public float duration = 1f;
        public LoopType loopType = LoopType.Yoyo;
        public Ease easeType = Ease.InSine;

        void Awake()
        {
            if (trans == null) trans = GetComponent<Transform>();
            originalSize = trans.localScale;
            originalPosition = trans.position;
        }

        protected void OnEnable()
        {
            trans.localScale = originalSize;
            trans.position = originalPosition;
            DoAnimate();
        }

        protected void OnDisable()
        {
            trans.DOKill();
        }


        protected abstract void DoAnimate();
    }
}
