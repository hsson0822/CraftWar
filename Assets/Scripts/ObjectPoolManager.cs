using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{

    static ObjectPoolManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        PoolInit();
    }

    public static ObjectPoolManager GetInstance()
    {
        return instance;
    }

    public ObjectPool bulletPool;
    public ObjectPool effectPool;
    public ObjectPool ultimatePool;
    public ObjectPool coinPool;
    public ObjectPool enemyPool;
    public ObjectPool enemyBulletPool;

    public void PoolInit()
    {
        bulletPool.AllocateBullet();
        effectPool.AllocateEffect();
        ultimatePool.AllocateUltimate();
        coinPool.AllocateCoin();
        enemyPool.AllocateEnemy();
        enemyBulletPool.AllocateEnemyBullet();
    }
}
