using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{

    public void UpgradeBulletPower()
    {
        if (PlayerPrefs.GetInt("Gold") > (PlayerPrefs.GetInt("BulletPower") * 5000))
        {
            PlayerPrefs.SetInt("Gold", (PlayerPrefs.GetInt("Gold") - (PlayerPrefs.GetInt("BulletPower") * 5000)));
            StoreManager.GetInstance().UpdateGold();

            PlayerPrefs.SetInt("BulletPower", PlayerPrefs.GetInt("BulletPower") + 1);
            StoreManager.GetInstance().UpdateBulletPower();
            StoreManager.GetInstance().UpdateBulletPowerPrice();

        }
    }
    public void UpgradeBulletSpeed()
    {
        if (PlayerPrefs.GetInt("Gold") > (PlayerPrefs.GetInt("BulletSpeed") * 5000))
        {
            PlayerPrefs.SetInt("Gold", (PlayerPrefs.GetInt("Gold") - (PlayerPrefs.GetInt("BulletSpeed") * 5000)));
            StoreManager.GetInstance().UpdateGold();

            PlayerPrefs.SetInt("BulletSpeed", PlayerPrefs.GetInt("BulletSpeed") + 1);
            StoreManager.GetInstance().UpdateBulletSpeed();
            StoreManager.GetInstance().UpdateBulletSpeedPrice();
        }
    }
    public void UpgradeCraftSpeed()
    {
        if (PlayerPrefs.GetInt("Gold") > (PlayerPrefs.GetInt("CraftSpeed") * 5000))
        {
            PlayerPrefs.SetInt("Gold", (PlayerPrefs.GetInt("Gold") - (PlayerPrefs.GetInt("CraftSpeed") * 5000)));
            StoreManager.GetInstance().UpdateGold();

            PlayerPrefs.SetInt("CraftSpeed", PlayerPrefs.GetInt("CraftSpeed") + 1);
            StoreManager.GetInstance().UpdateCraftSpeed();
            StoreManager.GetInstance().UpdateCraftSpeedPrice();

        }
    }
}
