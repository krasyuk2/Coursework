using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarShadow : MonoBehaviour
{
    private Move _move;
    private Slider _slider;
    public Slider sliderHeal;
    private bool _isCalc = true;
    private void Awake()
    {
        _move = FindObjectOfType<Move>();
        _slider = GetComponent<Slider>();
    }

    void Start()
    {
        _slider.maxValue = _move.MaxHeal;
        _slider.value = _move.Heal;

    }

    private void Update()
    {
        if (sliderHeal.value < _slider.value)
        {
            if (_isCalc) StartCoroutine(CalcSlider());
        }

        if (sliderHeal.value > _slider.value)
        {
            if (_isCalc)
            {
                _slider.value = sliderHeal.value;
            }
        }
    }

    IEnumerator CalcSlider()
    {
        _isCalc = false;
        yield return new WaitForSeconds(0.3f);
        for (float i = _slider.value; i >= sliderHeal.value-2f; i -= 2f)
        {
            _slider.value = i;
            yield return new WaitForSeconds(0.00001f);
        }

        _isCalc = true;
    }
    
}
