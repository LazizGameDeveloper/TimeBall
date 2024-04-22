using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class BarController : MonoBehaviour
{
    public float FillAmount {
        get => _fillAmount;
        set => _fillAmount = Mathf.Clamp(value, 0, 1);
    }
    [Range(0f, 1f)]
    [SerializeField] private float _fillAmount;

    private Image[] _images;

    public void Awake()
    {
        _images = GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        SetImagesFillAmount(_fillAmount);
    }

    private void SetImagesFillAmount(float fillAmount)
    {
        foreach (var image in _images) 
            image.fillAmount = fillAmount;
    }
}
