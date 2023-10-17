using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    Text BulletPower;
    Text BulletSpeed;
    Text CraftSpeed;
    Text Gold;
    
    Text BulletPowerPrice;
    Text BulletSpeedPrice;
    Text CraftSpeedPrice;

    static StoreManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public static StoreManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        BulletPower = GameObject.Find("BulletPower").gameObject.GetComponent<Text>();
        BulletSpeed = GameObject.Find("BulletSpeed").gameObject.GetComponent<Text>();
        CraftSpeed = GameObject.Find("CraftSpeed").gameObject.GetComponent<Text>();
        Gold = GameObject.Find("GoldText").gameObject.GetComponent<Text>();

        UpdateBulletPower();
        UpdateBulletSpeed();
        UpdateCraftSpeed();
        UpdateGold();

        BulletPowerPrice = GameObject.Find("BulletPowerPrice").gameObject.GetComponent<Text>();
        BulletSpeedPrice = GameObject.Find("BulletSpeedPrice").gameObject.GetComponent<Text>();
        CraftSpeedPrice = GameObject.Find("CraftSpeedPrice").gameObject.GetComponent<Text>();

        UpdateBulletPowerPrice();
        UpdateBulletSpeedPrice();
        UpdateCraftSpeedPrice();
    }
    public void UpdateBulletPower() {
        BulletPower.text = PlayerPrefs.GetInt("BulletPower").ToString();
    }
    public void UpdateBulletSpeed() {
        BulletSpeed.text = PlayerPrefs.GetInt("BulletSpeed").ToString();
    }
    public void UpdateCraftSpeed() {
        CraftSpeed.text = PlayerPrefs.GetInt("CraftSpeed").ToString();
    }
    public void UpdateBulletPowerPrice() {
        BulletPowerPrice.text = (int.Parse(BulletPower.text) * 5000).ToString();
    }
    public void UpdateBulletSpeedPrice() {
        BulletSpeedPrice.text = (int.Parse(BulletSpeed.text) * 5000).ToString(); ;
    }
    public void UpdateCraftSpeedPrice() {
        CraftSpeedPrice.text = (int.Parse(CraftSpeed.text) * 5000).ToString(); ;
    }
    public void UpdateGold() {
        Gold.text = PlayerPrefs.GetInt("Gold").ToString();
    }

    public void SetGoldText(string s)
    {
        Gold.text = "Buy + 10000 GOLD " + s;
    }
}