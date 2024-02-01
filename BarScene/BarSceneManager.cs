using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarSceneManager : MonoBehaviour
{

    public GameObject player;
    public GameObject tutorialEnter;
    public GameObject pressETutorial;
    public float distancePressE;
    public Animator animatorLoading;

    public TMP_Text scoreTutorialEnter;
    public GameObject arrowTutorialEnter;

    public GameObject oneEnter;
    public GameObject pressEOne;

    public GameObject twoEnter;
    public GameObject pressETwo;

    private DialogManager _dialogManager;
    private Dialog _dialog;

    private void Awake()
    {
        _dialogManager = GetComponent<DialogManager>();
        _dialog = GetComponent<Dialog>();
    }

    IEnumerator Start()
    {
        if (PlayerPrefs.HasKey("TutorialScore"))
        {
            arrowTutorialEnter.SetActive(false);
            scoreTutorialEnter.text = PlayerPrefs.GetInt("TutorialScore") + "";
            scoreTutorialEnter.gameObject.SetActive(true);
            
            //Прошел обучение включаем первый уровень
            if (PlayerPrefs.GetInt("TutorialScore") >= 10)
            {
                oneEnter.SetActive(true);
            }
        }

        if (PlayerPrefs.HasKey("Win"))
        {
            if (PlayerPrefs.GetInt("Win") == 1)
            {
                _dialogManager.StartDialog(_dialog);
                StartCoroutine(_dialogManager.TypeSentence("You're cool, you beat my game, thank you for everything!"));
                yield return new WaitForSeconds(4f);
                _dialogManager.EndDialog();
                
                twoEnter.SetActive(true); // Запускаем второй уровень
                
                PlayerPrefs.SetInt("Win",0);

            }
        }

      
    }

    // Update is called once per frame
    void Update()
    {
        DisplayPressE(pressETutorial,tutorialEnter,"TutorialScene");
        if (oneEnter.activeSelf)
        {
            DisplayPressE(pressEOne, oneEnter,"OneScene");  
        }
        if (twoEnter.activeSelf)
        {
            DisplayPressE(pressETwo, twoEnter,"TwoScene");  
        }
        
    }

    void DisplayPressE(GameObject pressE,GameObject obj, string nameScene)
    {
        if (Vector2.Distance(player.transform.position, obj.transform.position) < distancePressE)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(LoadScene(nameScene));
            }
            pressE.SetActive(true);
        }
        else
        {
            pressE.SetActive(false);
        }
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
