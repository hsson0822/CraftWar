using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{

    public Transform[] tf;
    int bulletPower;

    private void Start()
    {
        bulletPower = PlayerPrefs.GetInt("BulletPower");
        StartCoroutine(AttackCor());
    }

    void Update()
    {
        //KeyCheck();
    }

    public void KeyCheck()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Bullet tempObj = ObjectPoolManager.GetInstance().bulletPool.PopObject().GetComponent<Bullet>();
        //    //Bullet tempObj = Instantiate(bullet);

        //    tempObj.transform.position = tf.transform.position;

        //    Vector2 pos = tf.position;
        //    tempObj.Shot(pos);
        //}
    }

    IEnumerator AttackCor()
    {
        while(true)
        {
            if (bulletPower == 1)
            {
                ShotBullet(tf[0]);

                yield return new WaitForSeconds(0.1f);
            }
            else if (bulletPower == 2)
            {
                ShotBullet(tf[1]);
                ShotBullet(tf[2]);

                yield return new WaitForSeconds(0.1f);
            }
            else if (bulletPower == 3)
            {
                ShotBullet(tf[0]);
                ShotBullet(tf[1]);
                ShotBullet(tf[2]);

                yield return new WaitForSeconds(0.1f);
            }
            else if (bulletPower == 4)
            {
                ShotBullet(tf[1]);
                ShotBullet(tf[2]);
                ShotBullet(tf[3]);
                ShotBullet(tf[4]);


                yield return new WaitForSeconds(0.1f);
            }
            else if (bulletPower == 5)
            {
                ShotBullet(tf[0]);
                ShotBullet(tf[1]);
                ShotBullet(tf[2]);
                ShotBullet(tf[3]);
                ShotBullet(tf[4]);

                yield return new WaitForSeconds(0.1f);
            }
        }

    }

    void ShotBullet(Transform tr)
    {
        Bullet tempObj = ObjectPoolManager.GetInstance().bulletPool.PopObject().GetComponent<Bullet>();
        tempObj.transform.position = tr.position;

        Vector2 pos = tr.position;
        tempObj.Shot(pos);
    }
}
