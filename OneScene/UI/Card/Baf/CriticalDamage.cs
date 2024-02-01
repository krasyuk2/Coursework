using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class CriticalDamage : MonoBehaviour
{
   public bool startBaf;
   public int countChance = 5;
   public void StartBaf()
   {
      startBaf = true;
   }
   
   public bool RandomDamage()
   {
      Random random = new Random();
      int randomValue = random.Next(0, countChance); 
      print(randomValue);
      if (randomValue == 2)
      {
         return true;
      }

      return false;
   }
}
