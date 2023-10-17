using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : PoolableObject
{

    GameObject scoreText;
    float bulletSpeed;

    bool collisionFlag = false;

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText");
        bulletSpeed = (float)PlayerPrefs.GetInt("BulletSpeed") * 0.2f;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed);
    }

    public void Shot(Vector2 pos)
    {
        transform.position = pos;
    }

    public void OnBecameInvisible()
    {
        if (collisionFlag == false)
        {
            Push();
        }
    }

    public void OnBecameVisible()
    {
        collisionFlag = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && collisionFlag == false)
        {
            collisionFlag = true;

            Push();
            scoreText.GetComponent<ScoreText>().AddScore();

            Effect bulletEffect = ObjectPoolManager.GetInstance().effectPool.PopObject().GetComponent<Effect>();
            bulletEffect.transform.position = transform.position;
            bulletEffect.ShowEffect(EffectType.EffectBulletParticle);

            Effect crashEffect = ObjectPoolManager.GetInstance().effectPool.PopObject().GetComponent<Effect>();
            crashEffect.transform.position = collision.gameObject.transform.position;
            crashEffect.ShowEffect(EffectType.EffectCrash);

            Coin coin = ObjectPoolManager.GetInstance().coinPool.PopObject().GetComponent<Coin>();
            coin.transform.position = collision.gameObject.transform.position;

            collision.gameObject.GetComponent<Enemy>().StopCor();
            collision.gameObject.GetComponent<Enemy>().ChangeFlag();
            collision.gameObject.GetComponent<Enemy>().Push();
        }

        if(collision.gameObject.tag == "Boss")
        {
            Push();
            scoreText.GetComponent<ScoreText>().AddScore();

            Effect bulletEffect = ObjectPoolManager.GetInstance().effectPool.PopObject().GetComponent<Effect>();
            bulletEffect.transform.position = transform.position;
            bulletEffect.ShowEffect(EffectType.EffectBulletParticle);

            collision.gameObject.GetComponent<Boss>().addDamage(1);
        }
    }
}
