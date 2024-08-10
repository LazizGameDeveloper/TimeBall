using PoolSystem.Main;
using PoolSystem.Service;
using UnityEngine;
using UnityEngine.Serialization;

public class MortarAttack : AttackBase
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _focusTime;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private Transform _bulletCreateTransform;
    [SerializeField] private Transform _hitPointView;
    [SerializeField] private PoolData<PoolObject> _bulletsPoolData;
    [SerializeField] private PoolData<PoolObject> _explosionVFXPoolData;
    
    private const float HitPointViewRadius = 2.5f;
    
    private PoolMono<PoolObject> _bulletsPool;
    private PoolMono<PoolObject> _explosionVFXPool;

    private bool _isFocusing;
    private float _focusPassedTime;

    private void Start()
    {
        _bulletsPool = Utils.GetPoolFromServiceLocator(_bulletsPoolData);
        _explosionVFXPool = Utils.GetPoolFromServiceLocator(_explosionVFXPoolData);
        
        var scale = _radius / HitPointViewRadius;
        _hitPointView.transform.localScale = new Vector3(scale, scale, scale);
        ReloadBar.SetImagesActive(false);
    }

    private void Update()
    {
        PassedAttackTime += Time.deltaTime;
        RefreshReloadBar();
        
        if (TryAttack())
        {
            ReloadBar.SetImagesActive(false);
            _isFocusing = false;
            _focusPassedTime = 0;
            PassedAttackTime = 0;
        }
    }

    protected override void Attack()
    {
        var go = new GameObject("ex").AddComponent<Explosion>();
        var position = _hitPointView.transform.position;
        go.Explode(position, HitPointViewRadius, _hitMask);
        _explosionVFXPool.GetFromPool(position);
    }

    private bool TryAttack()
    {
        if (PassedAttackTime < AttackRate && !_isFocusing)
            return false;

        _isFocusing = true;

        if (_focusPassedTime == 0)
        {
            ReloadBar.SetImagesActive(true);
            SetShotPoint();
        }

        _focusPassedTime += Time.deltaTime;
        if (_focusPassedTime >= _focusTime)
        {
            Attack();
            return true;
        }
        return false;
    }

    private void SetShotPoint()
    {
        _hitPointView.transform.position = _target.position.AddY(-0.45f);
    }

    protected override void RefreshReloadBar()
    {
        ReloadBar.FillAmount = _focusPassedTime / _focusTime;
    }
}
