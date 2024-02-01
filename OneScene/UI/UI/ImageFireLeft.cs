using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ImageFireLeft : MonoBehaviour
{

    private Slider _slider;
    private Weapon _weapon;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _weapon = FindObjectOfType<Weapon>();
    }

    private void Update()
    {
        _slider.maxValue = _weapon.fireRate; // Типа может быть увеличен темп стрельбы бафом
        _slider.value = _weapon.FireTime;
    }

   

    
}
