using System;
using System.Collections.Generic;
using System.Linq;
using GigaCreation.Tools.Service;
using PoolSystem.Main;
using UnityEngine;

namespace PoolSystem.Service
{
    public class PoolService<T> : IService where T: MonoBehaviour 
    {
        private List<PoolMono<T>> _pools = new();
        private Transform _containersHolder;
        
        public PoolService(string containerName = "Pools")
        {
            _containersHolder = new GameObject(containerName).transform;
        }
        
        public PoolMono<T> GetPool(T poolObject) => _pools[_pools.FindIndex(pool => pool.Prefab == poolObject)];

        public bool HasPool(T poolObject) => _pools.Any(poolMono => poolMono.Prefab == poolObject);
        
        public PoolMono<T> RegisterPool(T prefab, int count, Transform container = null, bool autoExpand = true)
        {
            if (prefab == null)
                throw new Exception("Prefab for pool cannot be null");
            container = CreateContainerIfNullAndReturnParent(container, prefab.name);
            var pool = new PoolMono<T>(prefab, count, container, autoExpand);
            _pools.Add(pool);
            return pool;
        }
        
        public PoolMono<T> GetOrRegisterPool(PoolData<T> poolData)
        {
            return HasPool(poolData.PoolObject) ? GetPool(poolData.PoolObject) : RegisterPool(poolData.PoolObject, poolData.Count, poolData.Container, poolData.AutoExpand);
        }
        
        public PoolMono<T> GetOrRegisterPool(T prefab, int count, Transform container = null, bool autoExpand = true)
        {
            return HasPool(prefab) ? GetPool(prefab) : RegisterPool(prefab, count, container, autoExpand);
        }

        private Transform CreateContainerIfNullAndReturnParent(Transform container, string containerName)
        {
            if (container == null)
                container = new GameObject(containerName).transform;
            container.parent = _containersHolder;
            return container;
        }
    }
}