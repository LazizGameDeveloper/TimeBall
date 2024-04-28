using UnityEngine;
using UnityEngine.Serialization;

public class Mortar : EnemyBase
{
    [SerializeField] private float _focusTime = 0.2f;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _hitMask;
    
    [Header("View")]
    [SerializeField] private Transform _bulletCreateTransform;
    [SerializeField] private Transform _hitPointView;

    [Header("Pool settings")]
    [SerializeField] private CollisionEffectPool _bulletsPool;
    [FormerlySerializedAs("_exposionEffect")] [SerializeField] private CollisionEffectPool _exposionEffectPool;
    private const float _hitPointViewRadius = 2.5f;
    
    private bool _isFocusing;
    private float _focusPassedTime;
    private BarController _bar;

    private void Start()
    {
        var scale = _radius / _hitPointViewRadius;
        _hitPointView.transform.localScale = new Vector3(scale, scale, scale);
        
        _bar = _hitPointView.GetComponent<BarController>();
        _bar.SetImagesActive(false);
    }

    private void Update()
    {
        SetReloadBarFillAmount();

        if (TryAttack())
        {
            _bar.SetImagesActive(false);    
            _isFocusing = false;
            _focusPassedTime = 0;
            PassedAttackTime = 0;
        }
    }

    private void SetReloadBarFillAmount()
    {
        _bar.FillAmount = _focusPassedTime / _focusTime;
    }

    public override void Attack()
    {
        var go = new GameObject("ex").AddComponent<Explosion>();
        var position = _hitPointView.transform.position;
        go.Explode(position, _hitPointViewRadius, _hitMask);
        _exposionEffectPool.GetFromPool(position);
    }

    private bool TryAttack()
    {
        AddTimeToAttack();

        if (PassedAttackTime < AttackRate && !_isFocusing)
            return false;

        _isFocusing = true;
        
        if (_focusPassedTime == 0)
        {
            _bar.SetImagesActive(true);
            SetShotPoint();
        }

        var shouldAttack = _focusPassedTime >= _focusTime;
        
        if (shouldAttack)
            Attack();
        
        return shouldAttack;
    }
    
    private void AddTimeToAttack()
    {
        if (_isFocusing)
            _focusPassedTime += Time.deltaTime;
        else 
            PassedAttackTime += Time.deltaTime;
    }

    private void SetShotPoint()
    {
        _hitPointView.transform.position = RotationTarget.position.AddY(-0.5f);
    }
}
