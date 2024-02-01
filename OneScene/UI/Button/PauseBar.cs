using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBar : MonoBehaviour
{
    public GameObject pause;
    public bool isPause;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                isPause = true;
                pause.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                OnClickResume();

            }
        }
    }

    public void OnClickResume()
    {
        isPause = false;
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
