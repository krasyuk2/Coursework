using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopInfoAndBuy : MonoBehaviour
{
    public TMP_Text nameItem;
    public TMP_Text infoItem;
    public TMP_Text priceItem;
    public Image imageItem;
    
    public int price;
    public GameObject currentItem;

    private StatisticsLvlTwo _statisticsLvlTwo;

    private void Awake()
    {
        _statisticsLvlTwo = FindObjectOfType<StatisticsLvlTwo>();
    }

    public void SetInfo(string name, string info, int price, Sprite imageItem, GameObject currentItem)
    {
        nameItem.text = name;
        this.currentItem = currentItem;
        this.price = price;
        infoItem.text = info;
        priceItem.text = price + "";
        this.imageItem.sprite = imageItem;
    }

    public void OnClickBuy()
    {
        if (_statisticsLvlTwo.coin >= price)
        {
            _statisticsLvlTwo.coin -= price;
            currentItem.GetComponent<ShopItem>().OnClickBuy();
        }
        else
        {
            print("Не хватает деняг");
        }
       
    }

}
