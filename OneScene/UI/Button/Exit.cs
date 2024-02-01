using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void OnClick()
    {
        Application.Quit();
    }

    private void Start()
    {
        Time.timeScale = 1;
    }
}
