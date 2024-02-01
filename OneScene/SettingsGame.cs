using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsGame : MonoBehaviour
{
    public GameObject prefabPause;
    private GameObject _canvas;
    public bool isPause = true;
    private void Awake()
    {
        _canvas = GameObject.FindWithTag("Canvas");
    }

    void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Instantiate(prefabPause, _canvas.transform);
                isPause = false;
            }
        }
    }
}
