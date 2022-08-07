using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class NewMainMenuView : NewUIView
    {
        [SerializeField] private CanvasGroup CanvasGroup;
        [SerializeField] private Image OverlayImage;

        private Vector3 _originalScale;
        private Sequence _viewAnimationSequence;
        private Tween _overlayFadeTween;
        
        private NewMainMenuState NewMainMenuState => UIViewState as NewMainMenuState;
        private UIViewConfig ViewConfig => NewMainMenuState.UIViewConfig;

        #region Mono Methods

        private void OnEnable()
        {
            CanvasGroup.DOFade(0, 0f);
            _originalScale = Container.localScale;
            Container.localScale += Vector3.one * ViewConfig.ViewScaleFactor;
        }

        private void OnDisable()
        {
            _viewAnimationSequence?.Kill();
            _overlayFadeTween?.Kill();
        }

        #endregion

        #region Overridden Methods

        public override IEnumerator Show()
        {
            yield return base.Show();

            _viewAnimationSequence?.Kill();
            _viewAnimationSequence = DOTween.Sequence()
                              .Join(CanvasGroup.DOFade(1, ViewConfig.ViewInDuration))
                              .Join(Container.DOScale(_originalScale, ViewConfig.ViewInDuration));
            _viewAnimationSequence.Play();
            yield break;
        }

        public override IEnumerator Hide()
        {
            _viewAnimationSequence?.Kill();
            _viewAnimationSequence = DOTween.Sequence()
                              .Join(CanvasGroup.DOFade(0, ViewConfig.ViewInDuration))
                              .Join(Container.DOScale(_originalScale + Vector3.one * ViewConfig.ViewScaleFactor, ViewConfig.ViewInDuration));

            yield return _viewAnimationSequence.Play().WaitForCompletion();

            yield return base.Hide();
        }

        #endregion

        public void FadeoutOverlay()
        {
            OverlayImage.DOFade(0, 2.5f)
                .OnComplete(() => OverlayImage.gameObject.SetActive(false));
        }
        
        public void StartGame()
        {
            NewMainMenuState.StartGame();
        }

        public void ShowSettings()
        {
            NewMainMenuState.ShowSettingsView();
        }

        public void ShowQuitGameView()
        {
            Application.Quit();
            // MainMenuState.ShowQuitGameView();
        }
    }
}