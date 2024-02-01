using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public string loadSceneName;
    public bool isLoading;
    public GameObject Loading;
    private Animator _animatorLoading;

    private void Awake()
    {
        if(Loading != null)  _animatorLoading = Loading.GetComponent<Animator>();
       
    }

    public void OnClick()
    {
        if (isLoading)
        {
            StartCoroutine(LoadScene());
        }
        else
        {
            SceneManager.LoadScene(loadSceneName);
        }
    }
    IEnumerator LoadScene()
    {
      
        _animatorLoading.SetBool(Animator.StringToHash("Start"),true);
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(loadSceneName);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
