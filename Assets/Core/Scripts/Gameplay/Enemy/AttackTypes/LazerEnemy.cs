using System;
using UnityEngine;
using PrimeTween;

public class LaserAttack : AttackBase
{
    [SerializeField] private float _attackFocusTime;
    [SerializeField] private Transform _rayCreateTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private PoolExample _hitEffectPoolExample;

    private bool _isFocusing;
    private float _focusTimeTimer;
    
    private float _rayAlphaCurrent;
    private float _rayAlphaMin = 0.2f;
    private float _rayAlphaMax = 1f;
    private float _rayWidthMax;
    private float _rayWidthMin;

    private RotateToTarget _rotateToTarget; 
    
    private void Start()
    {
        _rayAlphaCurrent = _rayAlphaMin;
        _rayWidthMin = _laser.startWidth;
        _rayWidthMax = _rayAlphaMin * 1f;
        _laser.SetPosition(0, _rayCreateTransform.localPosition);
        TryGetComponent(out _rotateToTarget);
    }

    private void Update()
    {
        if (!_isFocusing)
            PassedAttackTime += Time.deltaTime;
        
        RefreshReloadBar();
        if (TryAttack())
        {
            _focusTimeTimer = 0;
            PassedAttackTime -= AttackRate;
            _isFocusing = false;
            if (_rotateToTarget != null)
                _rotateToTarget.enabled = true;
        }
        DrawRay();
    }

    protected override void Attack()
    {
        if (Physics.Raycast(_rayCreateTransform.position, _rayCreateTransform.up, out var hit, _rayDistance, _layerMask))
        {
            if (hit.transform.TryGetComponent<BallController>(out var ballController))
            {
                _hitEffectPoolExample.GetFromPool(hit.transform.position);
                ballController.Die();
            }
        }
    }

    private bool TryAttack()
    {
        if (PassedAttackTime < AttackRate)
            return false;

        if (_rotateToTarget != null)
            _rotateToTarget.enabled = false;
        
        _isFocusing = true;
        if (_focusTimeTimer == 0)
            DrawHighlightedRay();

        _focusTimeTimer += Time.deltaTime;
        if (_focusTimeTimer > _attackFocusTime)
        {
            Attack();
            return true;
        }
        return false;
    }

    private void DrawHighlightedRay()
    {
        Sequence.Create(cycles: 2, CycleMode.Yoyo)
            .Group(Tween.Custom(_rayAlphaMin, _rayAlphaMax, duration: _attackFocusTime, onValueChange: newValue => _rayAlphaCurrent = newValue, Ease.InExpo)
            .Group(Tween.Custom(_rayWidthMin, _rayWidthMax, duration: _attackFocusTime, onValueChange: value => _laser.startWidth = value, Ease.InExpo)
            .Group(Tween.Custom(_rayWidthMin, _rayWidthMax, duration: _attackFocusTime, onValueChange: value => _laser.endWidth = value, Ease.InExpo))));
    }

    private void DrawRay()
    {
        SetRayAlpha(_rayAlphaCurrent);
        var layerMask = LayerMask.GetMask("Walls");
        Vector3 destination;
        if (Physics.Raycast(_rayCreateTransform.position, _rayCreateTransform.up, out var hit, _rayDistance, layerMask))
        {
            destination = new Vector3(0, 0, (hit.point - _rayCreateTransform.position).magnitude);
        }
        else
        {
            destination = new Vector3(0, 0, (_rayCreateTransform.up * _rayDistance).magnitude);
        }
        _laser.SetPosition(1, destination);
    }

    private void SetRayAlpha(float alpha)
    {
        var color = _laser.startColor;
        color.a = alpha;
        _laser.startColor = color;
        color = _laser.endColor;
        color.a = alpha;
        _laser.endColor = color;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_rayCreateTransform.position, _rayCreateTransform.position + _rayCreateTransform.up * _rayDistance);
    }
#endif
}

