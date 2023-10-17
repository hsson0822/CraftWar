using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBullet : PoolableObject
{

    GameObject GameOverCanvas;
    GameObject controller;

    Text finalGold;
    Text finalScore;

    [System.Obsolete]
    private void Start()
    {
        GameOverCanvas = GameObject.Find("GameOverCanvas").transform.Find("Panel").gameObject;
        controller = GameObject.Find("SimpleMobileInputCamera").transform.Find("SimpleMobileInputCanvas").gameObject;
    }

    public void callCor(Vector3 dir)
    {
        StartCoroutine(BulletMoveCor(dir));
    }

    IEnumerator BulletMoveCor(Vector3 dir)
    {
        while (true)
        {
            transform.Translate(dir.normalized * 0.01f);

            yield return new WaitForSeconds(0f);
        }
    }

    public void OnBecameInvisible()
    {
        //Destroy(gameObject);
        Push();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameOverCanvas.SetActive(true);
            controller.SetActive(false);
            Time.timeScale = 0f;
            GameManager.GetInstance().pause = true;

            finalGold = GameObject.Find("GameOverCanvas").transform.Find("Panel").transform.Find("Panel").transform.Find("FinalGoldText").gameObject.GetComponent<Text>();
            finalScore = GameObject.Find("GameOverCanvas").transform.Find("Panel").transform.Find("Panel").transform.Find("FinalScoreText").gameObject.GetComponent<Text>();

            finalGold.text = GameObject.Find("Canvas").transform.Find("GoldText").GetComponent<Text>().text;
            finalScore.text = GameObject.Find("Canvas").transform.Find("ScoreText").GetComponent<Text>().text;

            SaveScore();

            if (PlayerPrefs.GetInt("PlayCount") >= 3)
            {
                UnityAdsManager_Simple.Instance.ShowAd();
            }
            else
            {
                PlayerPrefs.SetInt("PlayCount", (PlayerPrefs.GetInt("PlayCount") + 1));
            }
        }
    }

    public void SaveScore()
    {
        int score = int.Parse(finalScore.text.ToString());

        if (score > PlayerPrefs.GetInt("First"))
        {
            PlayerPrefs.SetInt("Third", PlayerPrefs.GetInt("Second"));
            PlayerPrefs.SetInt("Second", PlayerPrefs.GetInt("First"));
            PlayerPrefs.SetInt("First", score);
        }
        else if(score > PlayerPrefs.GetInt("Second"))
        {
            PlayerPrefs.SetInt("Third", PlayerPrefs.GetInt("Second"));
            PlayerPrefs.SetInt("Second", score);
        }
        else if(score > PlayerPrefs.GetInt("Third"))
        {
            PlayerPrefs.SetInt("Third", score);
        }
    }
}
