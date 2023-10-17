using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{

    Text text;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        score = 0;

        text.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        score += 10;
        text.text = score.ToString();
    }
}
