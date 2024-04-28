using UnityEngine;
using PoolSystem.Main;
using System.Collections;

public class Bullet : PoolObject
{
    [SerializeField] private float _speed;

    private CollisionEffectPool _collisionEffectPool;
    private Rigidbody _rigidbody;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        _collisionEffectPool = transform.parent.GetComponentsInChildren<CollisionEffectPool>()[1];
    }

    private void OnEnable()
    {
        SetVelocity();
        StartCoroutine(EnableTrailRenderer());
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
        _trailRenderer.emitting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<BallController>(out var controller))
            controller.Die();

        _collisionEffectPool.GetFromPool(transform.position);
        gameObject.SetActive(false);
    }

    private void SetVelocity()
    {
        var cos = Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
        var sin = Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
        var direction = new Vector3(sin, 0, cos);

        _rigidbody.velocity = direction * _speed;
    }

    private IEnumerator EnableTrailRenderer()
    {
        yield return null;
        yield return null;
        _trailRenderer.emitting = true;
    }
}
