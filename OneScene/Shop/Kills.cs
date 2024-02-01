using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kills : MonoBehaviour
{
    public float KillCount;
    private TMP_Text _text;
    

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    

    void Update()
    {
        if (PlayerPrefs.HasKey("Kills")) KillCount = PlayerPrefs.GetFloat("Kills");
        _text.text = Convert.ToString(KillCount);
    }
}
