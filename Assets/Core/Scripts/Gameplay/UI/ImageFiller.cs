using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFiller : MonoBehaviour
{
    [Range(0.0f, 1.0f)] public float _from;
    [Range(0.0f, 1.0f)] public float _to;

    [SerializeField] private float _duration;
    [SerializeField] private float _blankStartTime;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        DoFillAmount(_from, _to, _duration);
    }

    public void DoFillAmount(float from, float to, float duration)
    {
        StartCoroutine(FillAmountRoutine(from, to, duration, _blankStartTime));
    }

    private IEnumerator FillAmountRoutine(float from, float to, float duration, float blankStartTime)
    {
        yield return new WaitForSecondsRealtime(blankStartTime);
        var passedTime = 0f;
        while (passedTime < duration)
        {
            passedTime += Time.unscaledDeltaTime;
            _image.fillAmount = Mathf.Lerp(from, to, passedTime);
            yield return null;
        }
    }
}
