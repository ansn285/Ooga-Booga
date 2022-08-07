using System.Collections;
using DG.Tweening;

using UnityEngine;

namespace UI.Views
{
    public class SettingsView : UIView
    {
        private SettingsViewState SettingsViewState => UIViewState as SettingsViewState;
        
        private Sequence _viewAnimationSequence;

        #region Mono Methods

        private void OnDisable()
        {
            _viewAnimationSequence?.Kill();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }

        #endregion

        #region Overridden Methods

        protected override IEnumerator PlayShowAnimation()
        {
            _viewAnimationSequence?.Kill();
            _viewAnimationSequence = DOTween.Sequence()
                              .Join(CanvasGroup.DOFade(1, ViewConfig.ViewInDuration))
                              .Join(Container.DOScale(_originalScale, ViewConfig.ViewInDuration))
                              .SetEase(ViewConfig.PanelInEase);
            _viewAnimationSequence.Play();
            yield break;
        }

        protected override IEnumerator PlayHideAnimation()
        {
            _viewAnimationSequence?.Kill();
            _viewAnimationSequence = DOTween.Sequence()
                              .Join(CanvasGroup.DOFade(0, ViewConfig.ViewInDuration))
                              .Join(Container.DOScale(_originalScale + Vector3.one * ViewConfig.ViewScaleFactor, ViewConfig.ViewInDuration))
                              .SetEase(ViewConfig.PanelInEase);

            yield return _viewAnimationSequence.Play().WaitForCompletion();
        }

        #endregion

        public void GotoMainMenu()
        {
            SettingsViewState.GotoMainMenu();
        }

        public void ResumeGame()
        {
            SettingsViewState.ResumeGame();
        }
    }
}