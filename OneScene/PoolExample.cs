using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolExample : MonoBehaviour
{
   public int countPool;
   public bool autoSize;
   public PoolObject prefab;
   private PoolMono<PoolObject> poolMono;
   private void Start()
   {
       poolMono = new PoolMono<PoolObject>(prefab, 3, this.transform);
      poolMono.autoSize = autoSize;
   }

   public void Update()
   {
      if (Input.GetButtonDown("Fire1"))
      {
         poolMono.GetFreeElement();
      }
   }
}
