using UnityEngine;

public class SlowmotionEffect : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _maxIntensity;

    private TimeManager _timeManager;

    private bool _isInitialized = false;
    private float _defaultIntensity;

    private void OnEnable()
    {
        if (_isInitialized)
            Subscribe();
    }

    private void OnDisable()
    {
        if (_isInitialized)
            Unsubscribe();
    }

    public void Initialize(TimeManager timeManager)
    {
        _timeManager = timeManager;
        _isInitialized = true;
        Subscribe();
    }

    private void Subscribe()
    {
        _timeManager.OnTimeSlowedEvent += DoSlowmotionEffect;
        _timeManager.OnTimeUnslowedEvent += UndoSlowmotionEffect;
    }

    private void Unsubscribe()
    {
        _timeManager.OnTimeSlowedEvent += DoSlowmotionEffect;
        _timeManager.OnTimeUnslowedEvent += UndoSlowmotionEffect;
    }

    private void DoSlowmotionEffect()
    {
    }

    private void UndoSlowmotionEffect()
    {
    }
}
