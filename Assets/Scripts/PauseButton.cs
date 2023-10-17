using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    GameObject pauseCanvas;
    GameObject controller;

    [System.Obsolete]
    private void Start()
    {
        pauseCanvas = GameObject.Find("PauseCanvas").transform.FindChild("Panel").gameObject;
        controller = GameObject.Find("SimpleMobileInputCamera").transform.FindChild("SimpleMobileInputCanvas").gameObject;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        GameManager.GetInstance().pause = true;
        pauseCanvas.SetActive(true);
        controller.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameManager.GetInstance().pause = false;
        pauseCanvas.SetActive(false);
        controller.SetActive(true);
    }
}
