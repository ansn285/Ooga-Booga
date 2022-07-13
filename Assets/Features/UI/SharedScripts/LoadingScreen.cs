using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup CanvasGroup;
        [SerializeField] private Image FillerImage;

        private void OnEnable()
        {
            FillerImage.DOFillAmount(1, 2f)
                .OnComplete(EndingSequence);
        }
        
        private void EndingSequence()
        {
            CanvasGroup.DOFade(0, .8f)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}