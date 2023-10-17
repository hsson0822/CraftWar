using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PoolableObject
{
    GameObject goldText;
    Rigidbody2D rigid;

    Vector2 dir;

    //public AudioSource coin;

    private void Start()
    {
        goldText = GameObject.Find("GoldText");
        rigid = GetComponent<Rigidbody2D>();

        float x = Random.Range(0, 360);
        float y = Random.Range(0, 360);
        dir = new Vector2(x, y);

        rigid.AddForce(dir);

        //coin = GameObject.Find("Coint Audio").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //coin.Play();
            goldText.GetComponent<GoldText>().AddGold(10);
            Push();
        }

        if(collision.gameObject.tag == "Wall")
        {
            
        }
    }
}
