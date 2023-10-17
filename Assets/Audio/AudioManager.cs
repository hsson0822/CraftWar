using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    public AudioSource click;
    public AudioSource coin;
    public AudioSource bullet;
    public AudioSource bomb;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
    }

    public void ClickSoundPlay()
    {
        click.Play();
    }

    public void CoinSoundPlay()
    {
        coin.Play();
    }
    public void BulletSoundPlay()
    {
        bullet.Play();
    }
    public void BombSoundPlay()
    {
        bomb.Play();
    }
}
