using GigaCreation.Tools.Service;
using PoolSystem.Main;
using PoolSystem.Service;
using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private FPSLimiter _fpsLimiter;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private SlowmotionEffect _slowmotionEffect;
    [SerializeField] private BallController _ballConroller;

    private void Awake()
    {
        RegisterServices();
        _fpsLimiter.Initialize();
        _slowmotionEffect.Initialize(_timeManager);
        _ballConroller.Initialize();
    }

    private void RegisterServices()
    {
        ServiceLocator.Register(new PoolService<PoolObject>());
    }
}
