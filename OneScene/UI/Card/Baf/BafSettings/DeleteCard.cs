using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCard : MonoBehaviour
{
    private float temp;
    private float tempTime;
    
    private void Start()
    {
        tempTime = 0;
        temp = 0;
    }

    private void Update()
    {
        if(temp > 2) Destroy(gameObject);
        if (Time.unscaledTime >= tempTime + 0.1f) 
        {
            tempTime = Time.unscaledTime;
            temp++;
        }
    }
}
