using System;
using UnityEngine;

namespace UI.InteractionPanel
{
    public class InteractionPanel : MonoBehaviour
    {
        [SerializeField] private Animator PanelAnimator;

        private void OnValidate()
        {
            PanelAnimator = transform.Find("InteractionButtonContainer").GetComponent<Animator>();
        }

        public void Init(Vector3 position)
        {
            transform.position = position;
            PanelAnimator.Play("InteractionButtonIn");
        }

        public void HidePanel()
        {
            PanelAnimator.Play("InteractionButtonOut");
            InteractionPanelPooler.AddToPool(this);
        }
    }
}