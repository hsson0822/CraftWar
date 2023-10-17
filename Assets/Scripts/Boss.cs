using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int hp { get; set; }

    Animator anim;

    private void Start()
    {
        hp = 99;
        anim = GetComponent<Animator>();
        anim.Play("BossStart");
    }

    private void FixedUpdate()
    {
        if(hp < 50)
        {
            anim.Play("MoveRight");
        }
        else if(hp < 100)
        {
            anim.Play("MoveLeft");
        }
    }

    public void addDamage(int value)
    {
        hp -= value;

        if (hp < 0)
            Destroy(gameObject);

        Debug.Log(hp);
    }
}
