using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetAmmoTurret : MonoBehaviour
{
    private StatisticsLvlTwo _statisticsLvlTwo;
    private GameObject _turretParent;
    public TMP_Text textPrice;
    private int _price = 0;
    public Slider slider;
    private int _kof;
    public TMP_Text textValue;
    private void Awake()
    {
        _statisticsLvlTwo = FindObjectOfType<StatisticsLvlTwo>();
        _turretParent = GameObject.Find("Turret");
    }

    private void OnEnable()
    {
       
    }

    private void Update()
    {
        _kof = (int)slider.value;
        textValue.text = _kof + "";
        _price = 0;
        for (int i = 0; i < _turretParent.transform.childCount; i++)
        {
            var turret = _turretParent.transform.GetChild(i).GetComponent<TurretBlock>();
            int priceOneAmmo =(int) Math.Ceiling(turret.PriceOneAmmo);
            if (turret.Ammo <= turret.AmmoMax - _kof)
            {
                _price += priceOneAmmo * _kof;
              
            }
            else
            {
                if (turret.Ammo < turret.AmmoMax)
                {
                    _price += (turret.AmmoMax - turret.Ammo) * priceOneAmmo;
                    
                }
            }

        }
        textPrice.text = _price + "";
    }

    public void OnClickResetAmmo()
    {

        if (_statisticsLvlTwo.coin >= _price)
        {
            _statisticsLvlTwo.coin -= _price;
            for (int i = 0; i < _turretParent.transform.childCount; i++)
            {
                var turret = _turretParent.transform.GetChild(i).GetComponent<TurretBlock>();
                if (turret.Ammo <= turret.AmmoMax - _kof)
                {
                    turret.ResetAmmo(_kof);
              
                }
                else
                {
                    if (turret.Ammo < turret.AmmoMax)
                    {
                        turret.ResetAmmo(turret.AmmoMax - turret.Ammo);
                    
                    }
                }

            }
        }
      
    }
}
