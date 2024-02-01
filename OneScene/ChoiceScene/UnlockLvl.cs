using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnlockLvl : MonoBehaviour
{
    public GameObject PreviousLvl;
    private StartLvl _startLvl;
    public Color colorLock;
    private void Awake()
    {
        _startLvl = PreviousLvl.GetComponent<StartLvl>();
    }

    public void Start()
    {
        if (_startLvl.currentLvl == _startLvl.maxLvl)
        {
            GetComponent<Button>().enabled = true;
        }
        else
        {
            GetComponent<Image>().color = colorLock;
        }
    }
}
