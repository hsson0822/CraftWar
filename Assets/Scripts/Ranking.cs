using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    Text first;
    Text second;
    Text third;

    private void Awake()
    {
        first = transform.Find("FirstScore").GetComponent<Text>();
        second = transform.Find("SecondScore").GetComponent<Text>();
        third = transform.Find("ThirdScore").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        first.text = PlayerPrefs.GetInt("First").ToString();
        second.text = PlayerPrefs.GetInt("Second").ToString();
        third.text = PlayerPrefs.GetInt("Third").ToString();
    }
}
