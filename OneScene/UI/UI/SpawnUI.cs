using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUI : MonoBehaviour
{
    public GameObject prefabImageLeft;
    public GameObject prefabImageRight;
    private GameObject _canvas;

    private void Awake()
    {
        _canvas = GameObject.FindWithTag("Canvas");
    }

    public void Spawn()
    {
        Instantiate(prefabImageLeft, _canvas.transform);
        Instantiate(prefabImageRight, _canvas.transform);
    }
}
