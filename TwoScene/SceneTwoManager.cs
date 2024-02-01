using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTwoManager : MonoBehaviour
{
    public GameObject prefabStartUI;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void OnClick()
    {
        Time.timeScale = 1;
    }
}
