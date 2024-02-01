using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public GameObject enable;
    public GameObject disable;

    public void OnClick()
    {
        enable.SetActive(true);
        disable.SetActive(false);
    }
}
