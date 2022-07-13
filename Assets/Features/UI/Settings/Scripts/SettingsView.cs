using System.Collections;
using DG.Tweening;

using UnityEngine;

namespace UI.Views
{
    public class SettingsView : UIView
    {
        [SerializeField] private CanvasGroup CanvasGroup;

        private SettingsViewState SettingsViewState => UIViewState as SettingsViewState;
        private Vector3 _originalScale;
        private Sequence _viewAnimationSequence;

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

        public override IEnumerator Show()
        {
            yield return base.Show();

            _viewAnimationSequence?.Kill();
            _viewAnimationSequence = DOTween.Sequence()
                              .Join(CanvasGroup.DOFade(1, ViewConfig.ViewInDuration))
                              .Join(Container.DOScale(_originalScale, ViewConfig.ViewInDuration))
                              .SetEase(ViewConfig.PanelInEase);
            _viewAnimationSequence.Play();
            yield break;
        }

        public override IEnumerator Hide()
        {
            _viewAnimationSequence?.Kill();
            _viewAnimationSequence = DOTween.Sequence()
                              .Join(CanvasGroup.DOFade(0, ViewConfig.ViewInDuration))
                              .Join(Container.DOScale(_originalScale + Vector3.one * ViewConfig.ViewScaleFactor, ViewConfig.ViewInDuration))
                              .SetEase(ViewConfig.PanelInEase);

            yield return _viewAnimationSequence.Play().WaitForCompletion();

            yield return base.Hide();
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