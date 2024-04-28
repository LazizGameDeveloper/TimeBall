using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    [SerializeField] private ParticleSystem _collectionEffect;
    [SerializeField] private int amount = 1;
    [SerializeField] private CollisionEffectPool _collisionPool;

    public void Collect()
    {
        Bank.AddCoins(this, amount);
        CreateEffect();
    }

    private void CreateEffect()
    {
        _collisionPool.GetFromPool(transform.position);
    }
}
