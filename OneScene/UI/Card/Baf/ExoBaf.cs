using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoBaf : MonoBehaviour
{
   private GameObject _player;
   public GameObject prefabExo;
   public int Damage = 10;
   private void Awake()
   {
      _player = GameObject.FindWithTag("Player");
   }

  

   public void StartBaf()
   {
       GameObject go =  Instantiate(prefabExo, _player.transform );
       go.transform.SetParent(_player.transform);
     
   }
}
