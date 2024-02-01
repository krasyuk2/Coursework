using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{


    public Color colorTrigger;
    private Color _startColor;
    private TMP_Text _tmpText;
    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
        _startColor = _tmpText.color;
    }

    private void OnMouseEnter()
    {
        _tmpText.color = colorTrigger;
    }

    private void OnMouseExit()
    {
        _tmpText.color = _startColor;
    }
}
