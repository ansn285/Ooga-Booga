using System.Collections;
using UnityEngine;

namespace UI.Views
{
    public class NewUIView : MonoBehaviour
    {
        [HideInInspector] public NewUIViewState UIViewState;

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