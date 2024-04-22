using UnityEngine;
using TMPro;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private float _waitingTime = 0.25f;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateFPS());
    }

    private IEnumerator UpdateFPS()
    {
        var waitingTime = new WaitForSecondsRealtime(_waitingTime);
        while (true)
        {
            var current = (int)(1f / Time.unscaledDeltaTime);
            _text.text = current.ToString();
            yield return waitingTime;
        }
    }
}
