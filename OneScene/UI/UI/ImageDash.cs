using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDash : MonoBehaviour
{
    private Slider _slider;
    private Dash _dash;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _dash = FindObjectOfType<Dash>();
    }
    
    private void Update()
    {
        _slider.maxValue = _dash.TimeRateDash; 
        _slider.value = _dash.TimeDash;
    }
}
