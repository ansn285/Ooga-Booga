using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace UI.Views
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] protected RectTransform Container;
        [SerializeField] protected Canvas Canvas;
        [SerializeField] protected CanvasGroup CanvasGroup;
     
        protected UIViewState UIViewState;
        protected Vector3 _originalScale;
        protected UIViewConfig ViewConfig => UIViewState.UIViewConfig;

        private void OnEnable()
        {
            Container.gameObject.SetActive(false);
        }

        public virtual void Init(UIViewState state)
        {
            UIViewState = state;
            
            CanvasGroup.DOFade(0, 0f);
            _originalScale = Container.localScale;
            Container.localScale += Vector3.one * ViewConfig.ViewScaleFactor;
        }

        public virtual void Show()
        {
            Container.gameObject.SetActive(true);
            StartCoroutine(PlayShowAnimation());
        }

        public virtual void Hide()
        {
            StartCoroutine(PlayHideAnimation());
        }

        protected virtual IEnumerator PlayShowAnimation()
        {
            yield break;
        }

        protected virtual IEnumerator PlayHideAnimation()
        {
            Container.gameObject.SetActive(false);
            yield break;
        }
    }
}