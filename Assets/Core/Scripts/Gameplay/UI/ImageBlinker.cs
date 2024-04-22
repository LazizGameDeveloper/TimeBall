using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextProBlinker : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private bool _isAlphaZero;
    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(BlinkRoutine(_duration));
    }

    private IEnumerator BlinkRoutine(float duration)
    {
        while (true)
        {
            var waitingTime = new WaitForSecondsRealtime(duration);
            var endValue = Convert.ToInt32(_isAlphaZero);
            _text.DOFade(endValue, duration);
            yield return waitingTime;
            endValue = Convert.ToInt32(!_isAlphaZero);
            _text.DOFade(endValue, duration);
            yield return waitingTime;
        }
    }
}
