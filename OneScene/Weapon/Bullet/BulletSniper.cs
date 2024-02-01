using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSniper : BulletLvlTwo
{
   protected override void Methods()
   {
      StartCoroutine(DeleteBullet());
   }

   IEnumerator DeleteBullet()
   {
      yield return new WaitForSeconds(3f);
      Destroy(gameObject);
   }
   
    
}
