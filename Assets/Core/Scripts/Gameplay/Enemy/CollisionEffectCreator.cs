using UnityEngine;

public class CollisionEffectCreator : MonoBehaviour
{
    [SerializeField] private CollisionEffectPool _poolEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
            _poolEffect.GetFromPool(transform.position);
    }
}
