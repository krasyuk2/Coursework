using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseTutorial : MonoBehaviour
{
    public GameObject pause;
    public bool isPause;

    public Animator animatorLoading;
    private TutorialSceneManager _tutorialSceneManager;

    private void Awake()
    {
        _tutorialSceneManager = FindObjectOfType<TutorialSceneManager>();
    }

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

    public void OnClickExitToMenu(string loadSceneName)
    {
        Time.timeScale = 1;
        _tutorialSceneManager.SaveKillsScore();
        SceneManager.LoadScene(loadSceneName);
    }
    public void Exit()
    {
        _tutorialSceneManager.SaveKillsScore();
        Application.Quit();
    }

    public void StartSceneCoroutine(string loadSceneName)
    {
        StartCoroutine(LoadScene(loadSceneName));
    }
    IEnumerator LoadScene(string loadSceneName)
    {
        
        animatorLoading.SetBool(Animator.StringToHash("Start"),true);
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(loadSceneName);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
