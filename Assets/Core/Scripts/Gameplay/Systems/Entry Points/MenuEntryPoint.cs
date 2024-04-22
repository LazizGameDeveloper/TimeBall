using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private FPSLimiter _fpsLimiter;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private SlowmotionEffect _slowmotionEffect;
    [SerializeField] private BallController _ballConroller;

    private void Awake()
    {
        _fpsLimiter.Initialize();
        _slowmotionEffect.Initialize(_timeManager);
        _ballConroller.Initialize();
    }
}
