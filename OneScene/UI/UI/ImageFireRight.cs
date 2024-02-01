using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFireRight : MonoBehaviour
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
        _slider.maxValue = _weapon.CoolDawnFire2Rate;
        _slider.value = _weapon.CoolDawnFire2Time;
    }
}
