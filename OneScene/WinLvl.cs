using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLvl : MonoBehaviour
{
    
    
    public Animator animatorLoading;
    public void Win()
    {
        PlayerPrefs.SetInt("Win",1);
        StartCoroutine(LoadScene("BarScene"));
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
