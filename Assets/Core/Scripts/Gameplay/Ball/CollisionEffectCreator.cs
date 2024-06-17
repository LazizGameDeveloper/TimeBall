using GigaCreation.Tools.Service;
using PoolSystem.Main;
using PoolSystem.Service;
using UnityEngine;

public class CollisionEffectCreator : MonoBehaviour
{
    [SerializeField] private PoolData<PoolObject> _poolData;
    private PoolMono<PoolObject> _pool;
    private void Start()
    {
        _pool = Utils.GetPoolFromServiceLocator(_poolData);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            _pool.GetFromPool(transform.position);
        }
    }
}
