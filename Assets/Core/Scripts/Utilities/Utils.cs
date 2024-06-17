using GigaCreation.Tools.Service;
using PoolSystem.Main;
using PoolSystem.Service;

public static class Utils
{
    public static PoolMono<PoolObject> GetPoolFromServiceLocator(PoolData<PoolObject> poolData)
    {
        return ServiceLocator.Get<PoolService<PoolObject>>().GetOrRegisterPool(poolData);
    }
}