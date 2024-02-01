using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceStatistics : MonoBehaviour
{
    public GameObject statistics;
    private TMP_Text _textKills;
    private Slider _sliderKills;
    private TMP_Text _textBuffs;
    private Slider _sliderBuffs;
    private TMP_Text _textDamages;
    private Slider _sliderDamage;
    private Statistics _statistics;

    private void Awake()
    {
        _textKills = statistics.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        _textBuffs = statistics.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        _textDamages = statistics.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        
        _sliderKills = statistics.transform.GetChild(0).gameObject.GetComponent<Slider>();
        _sliderBuffs = statistics.transform.GetChild(1).gameObject.GetComponent<Slider>();
        _sliderDamage = statistics.transform.GetChild(2).gameObject.GetComponent<Slider>();

        _statistics = FindObjectOfType<Statistics>();
        
        _sliderKills.maxValue = 120;
        _sliderBuffs.maxValue = _statistics.BufMax;
        _sliderDamage.maxValue = 10000;
        
    }

    private void Update()
    {
        _sliderKills.value = _statistics.Kills;
        _sliderBuffs.value = _statistics.BufCurrent;
        _sliderDamage.value = _statistics.Damage;


        _textKills.text = $"Kills: {_statistics.Kills}";
        _textBuffs.text = $"Buffs: {_statistics.BufCurrent}";
        _textDamages.text = $"Damage: {_statistics.Damage}";
    }
}
