using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlBar : MonoBehaviour
{

    private Slider _slider;
    private TMP_Text _text;
    private Statistics _statistics;
    private CardBafManager _cardBafManager;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _statistics = FindObjectOfType<Statistics>();
        _cardBafManager = FindObjectOfType<CardBafManager>();
      
        _text = transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        _slider.maxValue = _cardBafManager.NextBafCount;
        _slider.value = _statistics.PlayerExp;
        _text.text = $"{_statistics.PlayerExp}/{_cardBafManager.NextBafCount}";
    }
}
