using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExitChoiceLvl : MonoBehaviour
{
    public GameObject Loading;
    private Animator _animatorLoading;

    private void Awake()
    {
        _animatorLoading = Loading.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
      
        _animatorLoading.SetBool(Animator.StringToHash("Start"),true);
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("ChoiceLvlScene");
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
