using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    private Move _move;
    private Slider _slider;
    private TMP_Text _text;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _move = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        _text = transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        _slider.maxValue = _move.MaxHeal;
        _slider.value = _move.Heal;
        _text.text = $"{_move.Heal}/{_move.MaxHeal}";
    }
    
}
