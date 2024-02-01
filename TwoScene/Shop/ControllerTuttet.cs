using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTuttet : MonoBehaviour
{
   public GameObject addFields;
   public float distance;
   private GameObject _cursor;
   public GameObject turretPrefab;
   private ShopManager _shopManager;
   private GameObject prefabUI;
   private StatisticsLvlTwo _statisticsLvlTwo;


   private void Awake()
   {
 
      addFields = GameObject.Find("AddFieldsTurret");
      _cursor = GameObject.FindWithTag("Cursor");
      _shopManager = FindObjectOfType<ShopManager>();
      _statisticsLvlTwo = FindObjectOfType<StatisticsLvlTwo>();

   }

   private void Start()
   {
      prefabUI = GameObject.Find("UITurret");
      EnableFieldOrDisable(true);
      _shopManager.ExitShop();
      _shopManager.BlockOpenShop(false);
   }

   private void EnableFieldOrDisable(bool active)
   {
      for (int i = 0; i < addFields.transform.childCount; i++)
      {
         addFields.transform.GetChild(i).gameObject.SetActive(active);
      }
   }

   private void Update()
   {
      Move();

      if (Input.GetButtonDown("Fire2"))
      {
         _shopManager.BlockOpenShop(true);
         EnableFieldOrDisable(false);
         print("деняги возвращай");
         prefabUI.SetActive(false);
         Destroy(gameObject);
      }

      
   } 

   void Move()
   {
      bool isNull = true;
      for (int i = 0; i < addFields.transform.childCount; i++)
      {
         if (Vector2.Distance(addFields.transform.GetChild(i).transform.position, _cursor.transform.position) <
             distance)
         {
            transform.position = addFields.transform.GetChild(i).transform.position;
            isNull = false;
            if (Input.GetButtonDown("Fire1"))
            {
               _shopManager.BlockOpenShop(true);
               Instantiate(turretPrefab, addFields.transform.GetChild(i).transform.position, Quaternion.identity);
               Destroy(addFields.transform.GetChild(i).gameObject);
               EnableFieldOrDisable(false);
               prefabUI.SetActive(false);
               Destroy(gameObject);
            }
         }

         if (i == addFields.transform.childCount-1)
         {
            if (isNull) transform.position = _cursor.transform.position;
         }
      }
   }
}
