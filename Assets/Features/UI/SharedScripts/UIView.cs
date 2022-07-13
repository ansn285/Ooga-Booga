using System;

using System.Collections;

using UnityEngine;

namespace UI.Views
{
    public class UIView : MonoBehaviour
    {
        [NonSerialized] public UIViewState UIViewState;

        [SerializeField] protected UIViewConfig ViewConfig;
        [SerializeField] protected RectTransform Container;
        [SerializeField] protected Canvas Canvas;

        private void OnEnable()
        {
            Container.gameObject.SetActive(false);
        }

        public virtual IEnumerator Show()
        {
            Container.gameObject.SetActive(true);
            yield break;
        }

        public virtual IEnumerator Hide()
        {
            Container.gameObject.SetActive(false);
            yield break;
        }
    }
}