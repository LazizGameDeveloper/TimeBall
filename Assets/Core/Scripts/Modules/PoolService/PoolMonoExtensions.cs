using PoolSystem.Main;
using UnityEngine;

namespace PoolSystem.Service
{
    public static class PoolMonoExtensions
    {
        public static T GetFromPool<T>(this PoolMono<T> pool, Vector3 position) where T : MonoBehaviour
        {
            var poolObject = pool.GetFreeElement();
            poolObject.transform.position = position;
            return poolObject;
        }
    }
}