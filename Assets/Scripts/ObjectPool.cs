using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    Stack<PoolableObject> objStack;
    public PoolableObject obj;

    public int allocateCount;

    public void AllocateBullet()
    {
        objStack = new Stack<PoolableObject>();

        for(int i = 0; i < allocateCount;  ++i)
        {
            PoolableObject objTemp = null;

            objTemp = Instantiate(obj, transform);

            objTemp.SetPool(this);
            objStack.Push(objTemp);
        }
    }

    public void AllocateEffect()
    {
        objStack = new Stack<PoolableObject>();

        for (int i = 0; i < allocateCount; ++i)
        {
            PoolableObject objTemp = null;

            objTemp = Instantiate(obj, transform);
            ((Effect)objTemp).GetComponent<Effect>().Init();
            objTemp.SetPool(this);
            objStack.Push(objTemp);
        }
    }

    public void AllocateUltimate()
    {
        objStack = new Stack<PoolableObject>();

        for (int i = 0; i < allocateCount; ++i)
        {
            PoolableObject objTemp = null;

            objTemp = Instantiate(obj, transform);

            objTemp.SetPool(this);
            objStack.Push(objTemp);
        }
    }

    public void AllocateCoin()
    {
        objStack = new Stack<PoolableObject>();

        for (int i = 0; i < allocateCount; ++i)
        {
            PoolableObject objTemp = null;

            objTemp = Instantiate(obj, transform);

            objTemp.SetPool(this);
            objStack.Push(objTemp);
        }
    }
    
    public void AllocateEnemy()
    {
        objStack = new Stack<PoolableObject>();

        for (int i = 0; i < allocateCount; ++i)
        {
            PoolableObject objTemp = null;

            objTemp = Instantiate(obj, transform);

            objTemp.SetPool(this);
            objStack.Push(objTemp);
        }
    }

    public void AllocateEnemyBullet()
    {
        objStack = new Stack<PoolableObject>();

        for (int i = 0; i < allocateCount; ++i)
        {
            PoolableObject objTemp = null;

            objTemp = Instantiate(obj, transform);

            objTemp.SetPool(this);
            objStack.Push(objTemp);
        }
    }

    public PoolableObject PopObject()
    {
        PoolableObject tempObj = null;

        if (objStack.Count > 0)
        {
            tempObj = objStack.Pop();
            tempObj.gameObject.SetActive(true);
        }

        return tempObj;
    }

    public void PushObject(PoolableObject obj)
    {
        obj.gameObject.SetActive(false);
        objStack.Push(obj);
    }
}
