using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardTextManager : MonoBehaviour
{
   private ItemManager _itemManager;
   private string _price;
   private GameObject textObj;
   private string _text;
   private bool _intOrFloat;
   private Kills _kills;
   public TMP_Text priceText;

   private void Awake()
   {
       _kills = FindObjectOfType<Kills>();
   }

   private void Start()
   {
       textObj = transform.GetChild(0).gameObject;
       textObj.GetComponent<TMP_Text>().text = _text;
       priceText.text = Convert.ToString(_price);
   }

   public void RegItemId(ItemManager itemManager, string price, bool intOrFloat, string text)
   {
      _itemManager = itemManager;
      _price = price;
      _text = text;
   }
   public void OnClick()
   { 
         _itemManager.ClickTextCard();
       
    
      
        
   }
   public void SetTextPrice(string text)
   {
       priceText.text = text;
   }
   

   void Buy()
   {
       
   }
   
}
