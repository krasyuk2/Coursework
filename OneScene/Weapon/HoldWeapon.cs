using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldWeapon : MonoBehaviour
{
   private CameraGame _cameraGame;
   private GameObject _player;
   private GameObject _cursor;
   [Range(0f,2f)]
   public float distance;
   public int indexHold = -1;
   private void Awake()
   {
      _cameraGame = FindObjectOfType<CameraGame>();
      _player = GameObject.FindWithTag("Player");
      _cursor = GameObject.FindWithTag("Cursor");
   }
   
   private void Update()
   {
      Move();
      if (indexHold == 1)
      {
         LocalScale();
         Hold();
      }

      if (indexHold == 2)
      {
         HoldV2();
      }
      
  
   }

   void Move()
   {
      transform.position = _player.transform.position;
   }
   void Hold()
   {
      Vector3 dir = _cameraGame.ray.direction;
      float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, rotZ);
   }

   void HoldV2()
   {
      Ray ray = new Ray(_player.transform.position, _cursor.transform.position - _player.transform.position);
      if (transform.GetChild(0).gameObject != null)
      {
         transform.GetChild(0).gameObject.transform.position = ray.GetPoint(distance);
        
      }
   }

   void LocalScale()
   {
      for (int i = 0; i < transform.childCount; i++)
      {
         float localScaleStart = Mathf.Abs(transform.GetChild(i).transform.localScale.x);
         transform.GetChild(i).transform.localScale = new Vector3(localScaleStart,
            _player.transform.localScale.x * localScaleStart, localScaleStart);
         transform.GetChild(i).localPosition = new Vector3(distance, 0, 0);
      }
   
     
     

   }
}
