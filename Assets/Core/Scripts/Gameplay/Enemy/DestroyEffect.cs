using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class DestroyEffect : MonoBehaviour
{
    [SerializeField] private PoolExample _effectPool;
    private HealthComponent _health;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
    }

    private void OnEnable() => _health.OnDeath += CreateDestroyEffect;
    private void OnDisable() => _health.OnDeath += CreateDestroyEffect;

    private void CreateDestroyEffect()
    {
        _effectPool.GetFromPool(transform.position);
    }
}
