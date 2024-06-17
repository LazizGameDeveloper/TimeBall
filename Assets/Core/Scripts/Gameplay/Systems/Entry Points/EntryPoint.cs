using GigaCreation.Tools.Service;
using PoolSystem.Main;
using PoolSystem.Service;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private FPSLimiter _fpsLimiter;
    [SerializeField] private Restarter _restarter;
    [SerializeField] private Win _win;
    [SerializeField] private WinUIShower _winUI;
    [SerializeField] private EnemyNumberManager _enemyNumberManager;
    [SerializeField] private BallController _ballConroller;
    [SerializeField] private InputController _inputController;
    [SerializeField] private SlowmotionEffect _slowMotionEffect;
    [SerializeField] private TimeManager _timeManager;

    private void Awake()
    {
        RegisterServices();
        
        _fpsLimiter.Initialize();
        _ballConroller.Initialize();
        _enemyNumberManager.Initialize();
        _inputController.Initialize(_timeManager);
        _win.Initialize(_enemyNumberManager, _timeManager);
        _winUI.Initialize(_win);
        _restarter.Initialize(_ballConroller, _enemyNumberManager);
        _slowMotionEffect.Initialize(_inputController.TimeManager);
    }

    private void RegisterServices()
    {
        ServiceLocator.Register(new PoolService<PoolObject>());
    }
}
