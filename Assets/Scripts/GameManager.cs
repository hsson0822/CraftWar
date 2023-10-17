using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        DontDestroyOnLoad(this.gameObject);

        Load();

        pause = false;

        if(!(PlayerPrefs.HasKey("BulletPower")))
        {
            PlayerPrefs.SetInt("BulletPower", 1);
        }

        if(!(PlayerPrefs.HasKey("BulletSpeed")))
        {
            PlayerPrefs.SetInt("BulletSpeed", 1);
        }

        if(!(PlayerPrefs.HasKey("CraftSpeed")))
        {
            PlayerPrefs.SetInt("CraftSpeed",1);
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public int gold;
    
    public void Save()
    {
        PlayerPrefs.SetInt("Gold", gold);
    }

    public void Load()
    {
        gold = PlayerPrefs.GetInt("Gold");
    }

    public void OnDestroy()
    {
        Save();
    }

    public bool pause
    {
        get
        {
            return pause;
        }
        set
        {

        }
    }

    private void Update()
    {
        KeyCheck();
    }

    void KeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            int gold = PlayerPrefs.GetInt("Gold");
            gold += 5000;
            PlayerPrefs.SetInt("Gold", gold);
        }
    }
}
