using UnityEngine;
using UnityEngine.Serialization;

public class CollisionEffectCreator : MonoBehaviour
{
    [FormerlySerializedAs("_poolEffect")] [SerializeField] private PoolExample _poolExampleEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            _poolExampleEffect.GetFromPool(transform.position);
        }
    }
}
