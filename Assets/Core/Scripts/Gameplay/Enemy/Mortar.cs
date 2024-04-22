using System;
using UnityEngine;

public class Mortar : EnemyBase
{
    [SerializeField] private int _bulletNumber = 1;
    [SerializeField] private float _timeBtwAttack = 0.2f;
    
    [Space]
    [SerializeField] private Transform _bulletCreateTransform;

    [Header("Pool settings")]
    [SerializeField] private CollisionEffectPool _bulletsPool;
    [SerializeField] private CollisionEffectPool _deathEffectPool;


    private Vector3 _shotPoint;
    private bool _isAttacking;
    private float _passedTimeBtwAttack;
    private int _bulletsCreatedWhileAttacking;

    private void Start()
    {
        _passedTimeBtwAttack = _timeBtwAttack;
    }

    private void Update()
    {
        SetShotPoint();

        TryAttack();
    }

    public override void Attack()
    {
        base.Attack();
    }

    private bool TryAttack()
    {
        AddTimeToAttack();

        if (PassedAttackTime < AttackRate && !_isAttacking)
            return false;

        _isAttacking = true;
        
        if (_passedTimeBtwAttack >= _timeBtwAttack)
        {
            Attack();
            
            _bulletsCreatedWhileAttacking++;
            _passedTimeBtwAttack -= _timeBtwAttack;
            PassedAttackTime -= AttackRate / _bulletNumber;
            
            if (_bulletsCreatedWhileAttacking >= _bulletNumber)
            {
                _passedTimeBtwAttack = _timeBtwAttack;
                _bulletsCreatedWhileAttacking = 0;
                _isAttacking = false;
            }
        }
        return true;
    }
    
    private void AddTimeToAttack()
    {
        if (_isAttacking)
            _passedTimeBtwAttack += Time.deltaTime;
        else 
            PassedAttackTime += Time.deltaTime;
    }

    private void SetShotPoint()
    {
        _shotPoint = RotationTarget.position;
    }
}
