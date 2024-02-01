using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDinoDeadScore : MonoBehaviour
{
   private Camera _camera;
   public GameObject testTarget;
   [Range(10f, 30f)] public float distanceTargetPosZ;
   private void Awake()
   {
      _camera = Camera.main;
   }

   void Update()
   {
      Vector3 screenMousePos = Input.mousePosition;
      Vector3 wordMousePos = _camera.ScreenToWorldPoint(screenMousePos);
      testTarget.transform.position = new Vector3(wordMousePos.x, wordMousePos.y, distanceTargetPosZ);
      transform.LookAt(testTarget.transform.position);
   }
}
