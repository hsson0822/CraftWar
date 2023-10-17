using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    EffectBulletParticle,
    EffectCrash,
    EffectItem
}
public class Effect : PoolableObject
{

    Animator anim;

    public void Init()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowEffect(EffectType type)
    {
        StartCoroutine(EffectCoroutine(type, 1f));
    }

    IEnumerator EffectCoroutine(EffectType type, float time = 1f)
    {
        switch(type)
        {
            case EffectType.EffectBulletParticle:
                anim.Play("EffectBulletParticle", 0,0);
                time = 0.7f / anim.speed;
                break;

            case EffectType.EffectCrash:
                anim.Play("EffectCrash", 0, 0);
                time = 1f;
                break;

            case EffectType.EffectItem:
                anim.Play("EffectItem", 0, 0);
                time = 1f;
                break;
        }

        yield return new WaitForSeconds(time);

        Push();
    }
}
