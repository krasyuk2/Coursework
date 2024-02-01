using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBaf : MonoBehaviour
{
    public delegate (float,float) ButtonE();
    public delegate (float,float) ButtonQ();

    public float timeE;
    public float timeQ;

    private float _timeRateE;
    private float _timeRateQ;

    public Slider sliderTimeE;
    public Slider sliderTimeQ;
    
    private ButtonE _buttonE;
    private ButtonQ _buttonQ;

    public bool Q;
    public bool E;

    public void AddActiveQ(ButtonQ method)
    {
        _buttonQ = null;
        _buttonQ += method;
        Q = true;
        sliderTimeQ.gameObject.SetActive(true);

    }

    public void AddActiveE(ButtonE method)
    {
        _buttonE = null;
        _buttonE += method;
        E = true;
        sliderTimeE.gameObject.SetActive(true);
    }

    
    private void Update()
    {
        
        if (Q)
        {
            (timeQ, _timeRateQ) = _buttonQ.Invoke();
            sliderTimeQ.maxValue = _timeRateQ;
            sliderTimeQ.value = timeQ;
        }

        if (E)
        {
            (timeE, _timeRateE) = _buttonE.Invoke();
            sliderTimeE.maxValue = _timeRateE;
            sliderTimeE.value = timeE;
        }
  
     

    }
    
    
}
