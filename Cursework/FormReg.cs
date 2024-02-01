using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FormReg : MonoBehaviour
{
    public TMP_Text login;
    public TMP_Text password;
    public TMP_Text passwordTwo;
    private DataBase _dataBase;
    public GameObject plus;
    public GameObject prefabLog;
    private DisplayText _displayText;
    public TMP_InputField[] inputFields;
    private void Awake()
    {
        _displayText = FindObjectOfType<DisplayText>();
        _dataBase = FindObjectOfType<DataBase>();
    }
    private void OnEnable()
    {
        plus.SetActive(false);
        foreach (var i in inputFields)
        {
            i.Select();
            i.text = "";
        }
        
    }

    private void OnDisable()
    {
        foreach (var i in inputFields)
        {
            i.Select();
            i.text = "";
        }
    }


    public void EnterGeg()
    {
        if (password.text != passwordTwo.text)
        {
            print("Пароли не совпали");
            _displayText.Message("Пароли не совпали");
        }
        else
        {
            if (!plus.activeSelf)
            {
                if (_dataBase.AddNewUser(login.text, password.text))
                {
                    print("Успех");
                    OnClickExit();
                }
             
            }
            
        }
       
    }
    
    public void PointerExit()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        if (_dataBase.DetectedSpecialSymbols(login.text))
        {
            plus.SetActive(true);
        }
        else
        {
            print(login.text);
            if (_dataBase.DetectedLogin(login.text))
            {
                plus.SetActive(true);
                print("Логин уже существует");
               
            }
            else
            {
                print("false");
                plus.SetActive(false);
            }
        }
    }

    public void OnClickExit()
    { 
        foreach (var i in inputFields)
        {
            i.Select();
            i.text = "";
        }

        prefabLog.SetActive(true);
        gameObject.SetActive(false);
    }
}
