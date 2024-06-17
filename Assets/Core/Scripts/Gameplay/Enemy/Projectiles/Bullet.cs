using UnityEngine;
using PoolSystem.Main;
using System.Collections;
using PoolSystem.Service;

public class Bullet : PoolObject
{
    [SerializeField] private PoolData<PoolObject> _hitVFXData;
    
    private PoolMono<PoolObject> _hitVFXPool;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        _hitVFXPool = Utils.GetPoolFromServiceLocator(_hitVFXData);
    }

    private void OnEnable()
    {
        StartCoroutine(EnableTrailRenderer());
    }

    private void OnDisable()
    {
        _trailRenderer.emitting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent<BallController>(out var controller))
            controller.Die();

        _hitVFXPool.GetFromPool(transform.position);
        gameObject.SetActive(false);
    }

    private IEnumerator EnableTrailRenderer()
    {
        yield return null;
        yield return null;
        _trailRenderer.emitting = true;
    }
}
