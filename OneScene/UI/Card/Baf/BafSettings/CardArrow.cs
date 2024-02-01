using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardArrow : MonoBehaviour
{
    private GameObject _arrowOne;
    private GameObject _arrowTwo;
    public AudioSource selectSound;
    private Button _button;
    
    private void Awake()
    {
        _arrowOne = transform.GetChild(0).gameObject;
        _arrowTwo = transform.GetChild(1).gameObject;
        _button = GetComponent<Button>();
    }
    
    private void OnMouseEnter()
    {
        if (_button.enabled)
        {
            _arrowOne.SetActive(true);
            _arrowTwo.SetActive(true);
            if (selectSound != null) selectSound.Play();
            
        }

    }

    private void OnMouseExit()
    {
        if (_button.enabled)
        {
            _arrowOne.SetActive(false);
            _arrowTwo.SetActive(false);
            if (selectSound != null) selectSound.Stop();
        }
    }
}
