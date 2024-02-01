using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    private string _text;
    public TMP_Text text;
    public GameObject prefabDisplay;
    private void OnEnable()
    {
      
    }

    public void Message(string textMes)
    {
        text.text = textMes;
        prefabDisplay.SetActive(true);

    }
    public void OnClickOk()
    {
        prefabDisplay.SetActive(false);
    }
}
