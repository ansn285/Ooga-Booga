using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenuView : UIView
    {
        [SerializeField] private Image OverlayImage;

        private Sequence _viewAnimationSequence;
        private Tween _overlayFadeTween;
        
        private MainMenuState MainMenuState => UIViewState as MainMenuState;

        #region Mono Methods
        
        private void OnDisable()
        {
            _viewAnimationSequence?.Kill();
            _overlayFadeTween?.Kill();
        }

        #endregion

        #region Overridden Methods

        protected override IEnumerator PlayShowAnimation()
        {
            _viewAnimationSequence?.Kill();
            _viewAnimationSequence = DOTween.Sequence()
                .Join(CanvasGroup.DOFade(1, ViewConfig.ViewInDuration))
                .Join(Container.DOScale(_originalScale, ViewConfig.ViewInDuration));
            _viewAnimationSequence.Play();

            yield break;
        }

        protected override IEnumerator PlayHideAnimation()
        {
            _viewAnimationSequence?.Kill();
            _viewAnimationSequence = DOTween.Sequence()
                .Join(CanvasGroup.DOFade(0, ViewConfig.ViewInDuration))
                .Join(Container.DOScale(_originalScale + Vector3.one * ViewConfig.ViewScaleFactor, ViewConfig.ViewInDuration));

            yield return _viewAnimationSequence.Play().WaitForCompletion();
        }

        #endregion

        public void FadeoutOverlay()
        {
            OverlayImage.DOFade(0, 2.5f)
                .OnComplete(() => OverlayImage.gameObject.SetActive(false));
        }
        
        public void StartGame()
        {
            MainMenuState.StartGame();
        }

        public void ShowSettings()
        {
            MainMenuState.ShowSettingsView();
        }

        public void ShowQuitGameView()
        {
            Application.Quit();
            MainMenuState.ShowQuitGameView();
        }
    }
}