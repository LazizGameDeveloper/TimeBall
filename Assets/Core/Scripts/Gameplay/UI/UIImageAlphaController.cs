using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class UIAlphaController : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _endValue;
    [SerializeField] private float _duration;
    [SerializeField] private bool _isAlhaZeroWhenInactive;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup.DOFade(0, 0);
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _canvasGroup.DOFade(_endValue, _duration).SetUpdate(true);
    }

    private void OnDisable()
    {
        if (_isAlhaZeroWhenInactive)
            _canvasGroup.DOFade(0, 0);
    }
}
