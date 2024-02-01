using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCancel : MonoBehaviour
{
    private CardBafManager _cardBafManager;

    private void Awake()
    {
        _cardBafManager = FindObjectOfType<CardBafManager>();
    }

    public void OnClick()
    {
        _cardBafManager.CancelCard();
    }
}
