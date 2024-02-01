using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestoreBase : MonoBehaviour
{
    public GameObject _base;
    public TMP_Text textPrice;
    private int _price;
    private StatisticsLvlTwo _statisticsLvlTwo;
    private void OnEnable()
    {
        _price = 0;
        for (int i = 0; i < _base.transform.childCount; i++)
        {
            if (!_base.transform.GetChild(i).gameObject.activeSelf)
            {
                _price += 4;
            }
            else
            {
                _price += _base.transform.GetChild(i).GetComponent<Block>().PriceReset();
            }
        }

        textPrice.text = _price + "";

    }

    private void Awake()
    {
        _statisticsLvlTwo = FindObjectOfType<StatisticsLvlTwo>();
    }
    
    public void OnClick()
    {
        if (_statisticsLvlTwo.coin >= _price)
        {
            for (int i = 0; i < _base.transform.childCount; i++)
            {
                if (!_base.transform.GetChild(i).gameObject.activeSelf)
                {
                    _base.transform.GetChild(i).gameObject.SetActive(true);
                    
                }
                else
                {
                    _base.transform.GetChild(i).GetComponent<Block>().ResetBlock();
                }
            }
            _statisticsLvlTwo.coin -= _price;
            _price = 0;
            textPrice.text = 0 + "";

        }
    }
}
