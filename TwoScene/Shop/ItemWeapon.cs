using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWeapon : ShopItem
{
    public GameObject prefabWeapon;
    public GameObject weaponParent;


    private bool isBuy;
    public override void OnClickBuy()
    {
        if (!isBuy)
        {
            Instantiate(prefabWeapon, weaponParent.transform);
            isBuy = true;
        }
        else
        {
            FindObjectOfType<StatisticsLvlTwo>().coin += prise;
        }
    }
}
