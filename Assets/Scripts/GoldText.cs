using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    Text text;
    int gold;

    // Start is called before the first frame update
    void Start()
    {
        gold = GameManager.GetInstance().gold;
        text = GetComponent<Text>();
        text.text = gold.ToString();

    }

    public void AddGold(int value)
    {
        gold += value;
        text.text = gold.ToString();
    }
}
