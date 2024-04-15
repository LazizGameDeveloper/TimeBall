using UnityEngine;

[SelectionBase]
public class Turret : EnemyBase
{
    [Header("Unique properties")] 
    [SerializeField] private int _bulletNumber = 1;
    [SerializeField] private float _timeBtwAttack = 0.2f;
    [SerializeField] private float _bulletSpeed;
    
    [Space]
    [SerializeField] private Transform _bulletCreateTransform;
    [SerializeField] private Transform _rotationTarget;
    [SerializeField] private float _rotationSpeed;

    [Header("Pool settings")]
    [SerializeField] private CollisionEffectPool _bulletsPool;
    [SerializeField] private CollisionEffectPool _deathEffectPool;

    private bool _isAttacking;
    private float _passedTimeBtwAttack;
    private int _bulletsCreatedWhileAttacking;
    private BarController _barController;

    public void Start()
    {
        _passedTimeBtwAttack = _timeBtwAttack;
        _barController = GetComponentInChildren<BarController>();
    }

    private void Update()
    {
        LookAtTarget(_rotationTarget);
        TryAttack();
        
        _barController.FillAmount = PassedAttackTime / AttackRate;
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

    public override void Attack()
    {
        CreateBullet(_bulletCreateTransform.position, transform.rotation);
    }

    private void CreateBullet(Vector3 position, Quaternion rotation)
    {
        var createdBullet = _bulletsPool.Pool.GetFreeElement(false);
        createdBullet.transform.position = position;
        createdBullet.transform.rotation = rotation;
        createdBullet.gameObject.SetActive(true);
    }

    private void LookAtTarget(Transform target)
    {
        var direction = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, direction, _rotationSpeed * Time.deltaTime);
    }

    public override void Die()
    {
        _deathEffectPool.CreateObject(transform.position);
        OnEnemyDieEvent?.Invoke(this);
        gameObject.SetActive(false);
    }
}
