using PoolSystem.Main;
using UnityEngine;

public class CollisionEffectPool : MonoBehaviour
{
    public PoolMono<PoolObject> Pool { get; private set; }

    [SerializeField] private int _poolCount = 5;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private PoolObject _poolObject;
    [SerializeField] private Transform _container;

    private void Start()
    {
        Pool = new PoolMono<PoolObject>(_poolObject, _poolCount, _container);
        Pool.autoExpand = _autoExpand;
    }

    public PoolObject GetFromPool(Vector3 position)
    {
        var poolObject = Pool.GetFreeElement();
        poolObject.transform.position = position;
        return poolObject;
    }
}