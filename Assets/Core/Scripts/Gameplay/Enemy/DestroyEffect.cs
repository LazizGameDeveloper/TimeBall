using System;
using GigaCreation.Tools.Service;
using PoolSystem.Main;
using PoolSystem.Service;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class DestroyEffect : MonoBehaviour
{
    [SerializeField] private PoolData<PoolObject> _poolData;

    private PoolMono<PoolObject> _pool;
    private HealthComponent _health;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        _pool = Utils.GetPoolFromServiceLocator(_poolData);
    }

    private void OnEnable() => _health.OnDeath += CreateDestroyEffect;
    private void OnDisable() => _health.OnDeath += CreateDestroyEffect;

    private void CreateDestroyEffect()
    {
        _pool.GetFromPool(transform.position);
    }
}
