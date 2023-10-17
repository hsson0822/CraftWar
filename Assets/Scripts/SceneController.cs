using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SceneToPlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void SceneToStore()
    {
        SceneManager.LoadScene(2);
    }
    public void SceneToRecord()
    {
        SceneManager.LoadScene(3);
    }

    public void SceneToTitle()
    {
        GameObject goldText =  GameObject.Find("GoldText");
        GameManager.GetInstance().gold = int.Parse(goldText.gameObject.GetComponent<Text>().text);

        GameManager.GetInstance().pause = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void SceneToTitle2()
    {
        SceneManager.LoadScene(0);
    }

    public void GameRetry()
    {
        GameObject goldText = GameObject.Find("GoldText");
        GameManager.GetInstance().gold = int.Parse(goldText.gameObject.GetComponent<Text>().text);
        GameManager.GetInstance().Save();

        GameManager.GetInstance().pause = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
