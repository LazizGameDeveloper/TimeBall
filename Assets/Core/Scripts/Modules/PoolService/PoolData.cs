using System;
using PoolSystem.Main;
using UnityEngine;

namespace PoolSystem.Service
{
    [Serializable]
    public class PoolData<T>
    {
        public T PoolObject;
        public int Count;
        public bool AutoExpand;
        public Transform Container;
    }
}