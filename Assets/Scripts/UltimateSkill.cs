using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill : PoolableObject
{

    GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * 0.01f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Effect bulletEffect = ObjectPoolManager.GetInstance().effectPool.PopObject().GetComponent<Effect>();
            bulletEffect.transform.position = transform.position;
            bulletEffect.ShowEffect(EffectType.EffectBulletParticle);

            Effect crashEffect = ObjectPoolManager.GetInstance().effectPool.PopObject().GetComponent<Effect>();
            crashEffect.transform.position = collision.gameObject.transform.position;
            crashEffect.ShowEffect(EffectType.EffectCrash);

            Coin coin = ObjectPoolManager.GetInstance().coinPool.PopObject().GetComponent<Coin>();
            coin.transform.position = collision.gameObject.transform.position;

            collision.gameObject.GetComponent<Enemy>().Push();
            scoreText.GetComponent<ScoreText>().AddScore();
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            collision.gameObject.GetComponent<EnemyBullet>().Push();
            scoreText.GetComponent<ScoreText>().AddScore();
        }

        if (collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().addDamage(20);
            scoreText.GetComponent<ScoreText>().AddScore();
        }
    }

    public void Shot()
    {
        transform.position = new Vector3(0, -5, 0);
    }

    public void OnBecameInvisible()
    {
        Push();
    }
}
