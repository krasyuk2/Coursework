using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [HideInInspector]
    public Vector2 posCursor;
    
    private Camera _camera;
    private float _startZpos;

    private void Awake()
    {
        if (Camera.main != null) _camera = Camera.main;
 
    }
    private void Start()
    {
        UnityEngine.Cursor.visible = false;
        _startZpos = transform.position.z;
    }
    private void Update()
    {
        Display();
      
    }
    void Display()
    {
        Vector2 posMouse = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
        Vector3 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        posCursor = new Vector3(Math.Clamp(posMouse.x, min.x, max.x), Math.Clamp(posMouse.y, min.y, max.y));
        transform.position = new Vector3(posCursor.x, posCursor.y, _startZpos);
    }
    
}
