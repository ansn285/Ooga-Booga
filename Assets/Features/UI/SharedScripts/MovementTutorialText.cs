using AN.Variables;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MovementTutorialText : MonoBehaviour
{
    [SerializeField] private TextMeshPro TutorialText;

    [SerializeField] private Bool MovementTutorialDone;
    
    [SerializeField] private string InitialText;
    [SerializeField] private string IntermediaryText;
    [SerializeField] private string FinalText;

    private Sequence _textAnimationSequence;
    private bool _movedOnce = false;

    private void OnEnable()
    {
        gameObject.SetActive(!MovementTutorialDone);
        TutorialText.text = $"{InitialText}";
    }

    private void OnDisable()
    {
        _textAnimationSequence?.Kill();
    }

    private void OnValidate()
    {
        TutorialText = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !MovementTutorialDone)
        {
            OnFirstMovement();
            _movedOnce = true;
        }
        
        if (Input.GetKeyUp(KeyCode.W) && _movedOnce)
        {
            ShowLastText();
            _movedOnce = false;
        }
    }

    private void OnFirstMovement()
    {
        if (TutorialText.text == IntermediaryText) return;
        
        TutorialText.text = $"{IntermediaryText}";
    }
    
    private void ShowLastText()
    {
        _textAnimationSequence = DOTween.Sequence()
            .Join(TutorialText.DOFade(0, .3f).SetEase(Ease.OutQuint)
                .OnComplete(() => TutorialText.text = $"{FinalText}"))
            .Append(TutorialText.DOFade(1, .2f).SetEase(Ease.InQuint))
            .OnComplete(() => MovementTutorialDone.SetValue(true));

        _textAnimationSequence.Play();
    }
}