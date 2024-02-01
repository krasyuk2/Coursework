using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
   public string name;
      public int prise;
   public Sprite spriteItem;
   [TextArea]   public string textItem;

   private ShopInfoAndBuy _shopInfoAndBuy;
   private void Awake()
   {
      _shopInfoAndBuy = FindObjectOfType<ShopInfoAndBuy>();
   }

   public void OnClick()
   {
      _shopInfoAndBuy.SetInfo(name,textItem,prise,spriteItem,gameObject);
   }

   public virtual void OnClickBuy() // Вызывется из ShopInfoAndBuy если хватает деняг)
   {
      print("Купили");
   }
}
