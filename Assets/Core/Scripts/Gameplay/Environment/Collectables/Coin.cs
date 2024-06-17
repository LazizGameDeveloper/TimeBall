using PoolSystem.Main;
using PoolSystem.Service;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    [SerializeField] private int _amount = 1;
    [SerializeField] private PoolData<PoolObject> _collectVFXData;

    private PoolMono<PoolObject> _collectVFXPool;

    private void Start()
    {
        _collectVFXPool = Utils.GetPoolFromServiceLocator(_collectVFXData);
    }

    public void Collect()
    {
        Bank.AddCoins(this, _amount);
        CreateEffect();
    }

    private void CreateEffect()
    {
        _collectVFXPool.GetFromPool(transform.position);
    }
}
