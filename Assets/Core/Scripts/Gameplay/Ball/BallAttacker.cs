using UnityEngine;

public class BallAttacker : MonoBehaviour
{
    [SerializeField] private int _damage = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<HealthComponent>(out var enemy))
            enemy.ApplyDamage(_damage);
    }
}
