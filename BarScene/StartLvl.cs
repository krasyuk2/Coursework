using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLvl : MonoBehaviour
{
   public string loadSceneName;
   public int maxLvl;
   public int currentLvl;
   public string idSaveLvl;
   public TMP_Text text;
   public GameObject Loading;
   private Animator _animatorLoading;
   public void OnClick()
   {
      StartCoroutine(LoadScene());
   }

   private void Awake()
   {
      _animatorLoading = Loading.GetComponent<Animator>();
      if (PlayerPrefs.HasKey(idSaveLvl))
      {
         currentLvl =(int) PlayerPrefs.GetFloat(idSaveLvl);
         if (currentLvl < 10)
         {
            text.text = $"0{currentLvl}/{maxLvl}";
         }
         else
         {
            text.text = $"{currentLvl}/{maxLvl}";
         }
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
