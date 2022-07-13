using System.Collections;

using UnityEngine;

namespace UI.Views
{
    public class GameHudFooter : MonoBehaviour
    {
        [SerializeField] private RectTransform Container;
        [SerializeField] private UIViewConfig ViewConfig;
        [SerializeField] private CanvasGroup CanvasGroup;

        public IEnumerator Show()
        {
            yield break;
        }

        public IEnumerator Hide()
        {
            yield break;
        }
    }
}