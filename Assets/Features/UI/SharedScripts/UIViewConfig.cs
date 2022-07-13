using DG.Tweening;

using UnityEngine;

namespace UI.Views
{
    [CreateAssetMenu(fileName = "UIViewConfig", menuName = "Views/UIConfig")]
    public class UIViewConfig : ScriptableObject
    {
        [SerializeField] public float ViewInDuration = .8f;
        [SerializeField] public float ViewOutDuration = .8f;
        [SerializeField] public float ViewScaleFactor = .3f;

        [SerializeField] public Ease PanelInEase = Ease.OutBack;
        [SerializeField] public Ease PanelOutEase = Ease.InBack;
    }
}