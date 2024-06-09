using UnityEngine;
using UnityEngine.Serialization;

public class Coin : MonoBehaviour, ICollectable
{
    [SerializeField] private ParticleSystem _collectionEffect;
    [SerializeField] private int amount = 1;
    [FormerlySerializedAs("_collisionPool")] [SerializeField] private PoolExample _collisionPoolExample;

    public void Collect()
    {
        Bank.AddCoins(this, amount);
        CreateEffect();
    }

    private void CreateEffect()
    {
        _collisionPoolExample.GetFromPool(transform.position);
    }
}
