using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableObject
{

    Coroutine cor;
    bool outFlag = false;

    public void ChangeFlag()
    {
        if (outFlag)
            outFlag = false;
        else
            outFlag = true;
    }

    public void MoveStraight(float time = 0)
    {
        cor = StartCoroutine(MoveStartightCor(time));
    }


    public void MoveCurveRight()
    {
        
    }

    public void MoverCurveLeft()
    {

    }

    public void OnBecameInvisible()
    {
        if (outFlag)
        {
            StopCor();
            Push();
            outFlag = false;
        }
    }

    public void StopCor()
    {
        StopCoroutine(cor);
    }

    IEnumerator MoveStartightCor(float time)
    {
        yield return new WaitForSeconds(time);

        while (true)
        {
            transform.Translate(Vector3.down * 0.005f);


            yield return new WaitForSeconds(0f);
        }
    }
}
