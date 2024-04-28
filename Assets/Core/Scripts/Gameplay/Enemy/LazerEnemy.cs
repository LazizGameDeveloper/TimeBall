using UnityEngine;
using PrimeTween;

public class LazerEnemy : EnemyBase
{
    [SerializeField] private float _attackFocusTime;
    [SerializeField] private Transform _rayCreateTransform;
    
    [Header("Unique properties")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance;
    [SerializeField] private LineRenderer _lazer;
    
    [Header("Pool settings")]
    [SerializeField] private CollisionEffectPool _deathEffectPool;
    [SerializeField] private CollisionEffectPool _hitEffectPool;

    private bool _isFocusing;
    private float _focusTimeTimer;
    private BarController _reloadBar;

    private readonly float _rayAlphaMin = 0.2f;
    private readonly float _rayAlphaMax = 1f;
    private float _rayAlphaCurrent;
    
    private float _rayWidthMax;
    private float _rayWidthMin;
    

    private void Start()
    {
        _rayAlphaCurrent = _rayAlphaMin;
        _rayWidthMin = _lazer.startWidth;
        _rayWidthMax = _rayAlphaMin * 1f;
        
        _lazer.SetPosition(0, _rayCreateTransform.localPosition);
        _reloadBar = GetComponentInChildren<BarController>();
    }

    private void Update()
    {
        AddTickToAttackTimers();
        RefreshReloadBar();
        DrawRay();
        
        if (TryAttack())
        {
            _focusTimeTimer = 0;
            PassedAttackTime -= AttackRate;
            _isFocusing = false;
        }
        
        if (!_isFocusing)
            LookAtTarget(RotationTarget);
    }

    private bool TryAttack()
    {
        if (PassedAttackTime < AttackRate)
            return false;

        _isFocusing = true;
        if (_focusTimeTimer == 0)
            DrawHighlightedRay();
        
        var hasFocused = _focusTimeTimer > _attackFocusTime; 
        if (hasFocused)
            Attack();
        return hasFocused;
    }

    public override void Attack()
    {
        if(Physics.Raycast(_rayCreateTransform.position, _rayCreateTransform.up, out var hit, _rayDistance,_layerMask))
        {
            if (hit.transform.TryGetComponent<BallController>(out var ballController))
            {
                _hitEffectPool.GetFromPool(hit.transform.position);
                ballController.Die();
            }
        }
    }

    public override void Die()
    {
        _deathEffectPool.GetFromPool(transform.position);
        OnEnemyDieEvent?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void DrawHighlightedRay()
    {
        Sequence.Create(cycles: 2, CycleMode.Yoyo)
            .Group(Tween.Custom(_rayAlphaMin, _rayAlphaMax, duration: _attackFocusTime,
                onValueChange: newValue => _rayAlphaCurrent = newValue, Ease.InExpo)
            .Group(Tween.Custom(_rayWidthMin, _rayWidthMax, duration: _attackFocusTime,
                onValueChange: value => _lazer.startWidth = value, Ease.InExpo)
            .Group(Tween.Custom(_rayWidthMin, _rayWidthMax, duration: _attackFocusTime,
                onValueChange: value => _lazer.endWidth = value, Ease.InExpo))));
    }
    
    private void DrawRay()
    {
        SetRayAlpha(_rayAlphaCurrent);
        var layerMask = LayerMask.GetMask("Walls");
        if(Physics.Raycast(_rayCreateTransform.position, _rayCreateTransform.up, out var hit, _rayDistance, layerMask))
        {
            var destination = new Vector3(0, 0, (hit.point - _rayCreateTransform.position).magnitude);
            _lazer.SetPosition(1, destination);
        }
    }
    
    private void AddTickToAttackTimers()
    {
        if (_isFocusing)
            _focusTimeTimer += Time.deltaTime;
        else
            PassedAttackTime += Time.deltaTime;
    }

    private void RefreshReloadBar()
    {
        _reloadBar.FillAmount = PassedAttackTime / AttackRate;
    }

    private void SetRayAlpha(float a)
    {
        var color = _lazer.startColor;
        color.a = a;
        _lazer.startColor = color;
        color = _lazer.endColor;
        color.a = a;
        _lazer.endColor = color;
    }
}
