using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FormLog : MonoBehaviour
{
    public TMP_Text login;
    public TMP_Text password;
    private DataBase _dataBase;
    public GameObject prefabReg;
    private DisplayText _displayText;
    public TMP_InputField[] inputFields;
    private void Awake()
    {
        _dataBase = FindObjectOfType<DataBase>();
        _displayText = FindObjectOfType<DisplayText>();
    }

    private void OnEnable()
    {
        foreach (var i in inputFields)
        {
            i.Select();
            i.text = "";
        }

    }

    public void EnterLog()
    {
        if (_dataBase.AuthUser(login.text, password.text))
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            print("Пользователя не существует");
            _displayText.Message("Пользователя не существует");
        }
    }

    public void OnClickReg()
    {
        prefabReg.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
}
