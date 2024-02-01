using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseLvlTwo : MonoBehaviour
{
  
    public GameObject pause;
    public bool isPause;
    private ShopManager _shopManager;
    public Animator animatorLoading;

    private void Awake()
    {
        _shopManager = FindObjectOfType<ShopManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                _shopManager.BlockOpenShop(false);
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
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickResume()
    {
        Time.timeScale = 1;
        _shopManager.BlockOpenShop(true);
        isPause = false;
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickExitToMenu(string loadSceneName)
    {
        Time.timeScale = 1;
    
        SceneManager.LoadScene(loadSceneName);
    }
    public void Exit()
    {
      
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
