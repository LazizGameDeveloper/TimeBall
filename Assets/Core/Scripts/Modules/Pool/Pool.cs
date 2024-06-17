using UnityEngine;

namespace PoolSystem.Main
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private int _poolCount = 5;
        [SerializeField] private bool _autoExpand;
        [SerializeField] private PoolObject _poolObject;

        private PoolMono<PoolObject> _pool;
        
        private void Start()
        {
            _pool = new PoolMono<PoolObject>(_poolObject, _poolCount, transform);
            _pool.AutoExpand = _autoExpand;
        }

        public void GetFromPool(Vector3 position)
        {
            var effect = _pool.GetFreeElement();
            effect.transform.position = position;
        }
    }
}
