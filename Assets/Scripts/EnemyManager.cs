using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int patternNum;

    public Transform[] point;

    public GameObject craft;
    private int cnt;

    void Start()
    {
        StartCoroutine(PatternCor());
        cnt = 0;
    }

    IEnumerator PatternCor()
    {
        while (true)
        {
            patternNum = Random.Range(0, 3);
            
            if(cnt== 20)
                patternNum = 3;

            switch(patternNum)
            {
                case 0:
                    StraightLine();
                    break;

                case 1:
                    StraightCheck();
                    break;

                case 2:
                    StraightRandom();
                    break;

                case 3:
                    CallBoss();
                    break;


            }

            yield return new WaitForSeconds(3f);
            ++cnt;
        }
    }

    public void StraightLine()
    {
        for(int i = 0; i < 5; ++i)
        {
            Enemy tempObj = ObjectPoolManager.GetInstance().enemyPool.PopObject().GetComponent<Enemy>();

            tempObj.transform.position = point[i].position;
            tempObj.MoveStraight();
            tempObj.ChangeFlag();

            ShootDir(tempObj);
        }
    }

    public void StraightCheck()
    {
        float count = 2;

        for (int i = 0; i < 5; ++i)
        {
            Enemy tempObj = ObjectPoolManager.GetInstance().enemyPool.PopObject().GetComponent<Enemy>();

            tempObj.transform.position = point[i].position;
            tempObj.MoveStraight(count);

            if (i < 2)
                --count;
            else
                ++count;

            tempObj.ChangeFlag();

            ShootDir(tempObj);
        }
    }

    public void StraightRandom()
    {
        float ranTime;

        for (int i = 0; i < 5; ++i)
        {
            Enemy tempObj = ObjectPoolManager.GetInstance().enemyPool.PopObject().GetComponent<Enemy>();

            ranTime = Random.Range(0f, 2f);

            tempObj.transform.position = point[i].position;
            tempObj.MoveStraight(ranTime);
            tempObj.ChangeFlag();

            ShootDir(tempObj);
        }
    }

    public void ShootDir(Enemy enemyObj)
    {
        EnemyBullet enemyBulletObj = ObjectPoolManager.GetInstance().enemyBulletPool.PopObject().GetComponent<EnemyBullet>();
        enemyBulletObj.transform.position = enemyObj.transform.GetChild(0).position;


        int shootDir = Random.Range(0, 2);


        switch(shootDir)
        {
            case 0:
                enemyBulletObj.callCor(Vector3.down);
                break;

            case 1:
                Vector3 dir = craft.transform.position - enemyObj.transform.position;
                enemyBulletObj.callCor(dir);
                break;
        }
    }

    public void CallBoss()
    {
        Boss tempObj;
       // tempObj.transform.position = point[2].position;
    }
}
