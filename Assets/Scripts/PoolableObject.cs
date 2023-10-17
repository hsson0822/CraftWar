using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    ObjectPool objPool;

    public void SetPool(ObjectPool pool)
    {
        objPool = pool;
        gameObject.SetActive(false);
    }

    public void Push()
    {
        objPool.PushObject(this);
    }
}
